using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Digitalizacion2014.Scan.Images.Transforms;

namespace Digitalizacion2014.Scan.Images
{
    public class ScannedImageList
    {
        public ScannedImageList()
        {
            Images = new List<IScannedImage>();
        }

        public List<IScannedImage> Images { get; private set; }

        public IEnumerable<int> MoveUp(IEnumerable<int> selection)
        {
            var newSelection = new int[selection.Count()];
            int lowerBound = 0;
            int j = 0;
            foreach (int i in selection.OrderBy(x => x))
            {
                if (i != lowerBound++)
                {
                    IScannedImage img = Images[i];
                    Images.RemoveAt(i);
                    Images.Insert(i - 1, img);
                    img.MovedTo(i - 1);
                    newSelection[j++] = i - 1;
                }
                else
                {
                    newSelection[j++] = i;
                }
            }
            return newSelection;
        }

        public IEnumerable<int> MoveDown(IEnumerable<int> selection)
        {
            var newSelection = new int[selection.Count()];
            int upperBound = Images.Count - 1;
            int j = 0;
            foreach (int i in selection.OrderByDescending(x => x))
            {
                if (i != upperBound--)
                {
                    IScannedImage img = Images[i];
                    Images.RemoveAt(i);
                    Images.Insert(i + 1, img);
                    img.MovedTo(i + 1);
                    newSelection[j++] = i + 1;
                }
                else
                {
                    newSelection[j++] = i;
                }
            }
            return newSelection;
        }

        public IEnumerable<int> RotateFlip(IEnumerable<int> selection, RotateFlipType rotateFlipType)
        {
            foreach (int i in selection)
            {
                Images[i].AddTransform(new RotationTransform(rotateFlipType));
                Images[i].UpdateThumbnail();
            }
            return selection.ToList();
        }

        public void Delete(IEnumerable<int> selection)
        {
            foreach (IScannedImage img in Images.ElementsAt(selection))
            {
                img.Dispose();
            }
            Images.RemoveAll(selection);
        }

        public IEnumerable<int> Interleave(IEnumerable<int> selection)
        {
            // Partition the image list in two
            int count = Images.Count;
            int split = (count + 1) / 2;
            var p1 = Images.Take(split).ToList();
            var p2 = Images.Skip(split).ToList();

            // Rebuild the image list, taking alternating images from each the partitions
            Images.Clear();
            for (int i = 0; i < count; ++i)
            {
                Images.Add(i % 2 == 0 ? p1[i / 2] : p2[i / 2]);
            }

            // Clear the selection (may be changed in the future to maintain it, but not necessary)
            return Enumerable.Empty<int>();
        }

        public IEnumerable<int> Deinterleave(IEnumerable<int> selection)
        {
            // Duplicate the list
            int count = Images.Count;
            int split = (count + 1) / 2;
            var images = Images.ToList();

            // Rebuild the image list, even-indexed images first
            Images.Clear();
            for (int i = 0; i < split; ++i)
            {
                Images.Add(images[i * 2]);
            }
            for (int i = 0; i < (count - split); ++i)
            {
                Images.Add(images[i * 2 + 1]);
            }

            // Clear the selection (may be changed in the future to maintain it, but not necessary)
            return Enumerable.Empty<int>();
        }

        public IEnumerable<int> Reverse()
        {
            Reverse(Enumerable.Range(0, Images.Count));

            // Selection is unpredictable, so clear it
            return Enumerable.Empty<int>();
        }

        public IEnumerable<int> Reverse(IEnumerable<int> selection)
        {
            var selectionList = selection.ToList();
            int pairCount = selectionList.Count / 2;

            // Swap pairs in the selection, excluding the middle element (if the total count is odd)
            for (int i = 0; i < pairCount; i++)
            {
                int x = selectionList[i];
                int y = selectionList[selectionList.Count - i - 1];
                var temp = Images[x];
                Images[x] = Images[y];
                Images[y] = temp;
            }

            // Selection stays the same, so is easy to maintain
            return selectionList;
        }

        public IEnumerable<int> ResetTransforms(IEnumerable<int> selection)
        {
            foreach (IScannedImage img in Images.ElementsAt(selection))
            {
                img.ResetTransforms();
                img.UpdateThumbnail();
            }
            return selection.ToList();
        }
    }
}
