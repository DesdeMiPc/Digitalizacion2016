using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Digitalizacion2014.ImportExport.Images
{
    public class ImageFileNamer
    {
        public IEnumerable<string> GetFileNames(string baseFileName, int imageCount)
        {
            if (imageCount == 0)
            {
                // Skip the unnecesary logic below if there are no images
                return Enumerable.Empty<string>();
            }

            // Split the baseFileName into two parts, between which the number (if any) will be placed
            string name = Path.GetFileNameWithoutExtension(baseFileName);
            string dir = Path.GetDirectoryName(baseFileName);
            string prefix = string.IsNullOrEmpty(dir) ? name : dir + "\\" + name;
            string postfix = Path.GetExtension(baseFileName);

            if (imageCount == 1)
            {
                // Don't show any number at all if there's only one image
                return Enumerable.Repeat(prefix + postfix, 1);
            }

            // The number of digits in each number (everything should be zero-padded to this)
            // Based on the number of images, e.g. (2-9 images -> 1 digit, 10-99 -> 2, 100-999 -> 3, etc.)
            int digits = (int)Math.Floor(Math.Log10(imageCount)) + 1;

            return Enumerable.Range(1, imageCount).Select(i => prefix + "_" + i.ToString("D" + digits) + postfix);
        }
    }
}
