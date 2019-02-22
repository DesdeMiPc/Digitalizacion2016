using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Digitalizacion2014.Scan.Images;

namespace Digitalizacion2014.Controles
{
    public partial class ThumbnailList : ListView
    {
        public ThumbnailList()
        {
            InitializeComponent();
            LargeImageList = ilThumbnailList;
        }

        public void UpdateImages(List<IScannedImage> images)
        {
            ilThumbnailList.Images.Clear();
            Clear();
            foreach (IScannedImage img in images)
            {
                AppendImage(img);
            }
        }

        public void AppendImage(IScannedImage img)
        {
            ilThumbnailList.Images.Add(img.Thumbnail);
            Items.Add((img.Tag == null ? "" : img.Tag.ToString()), ilThumbnailList.Images.Count - 1);
        }

        public void UpdateView(List<IScannedImage> images)
        {
            ilThumbnailList.Images.Clear();
            foreach (IScannedImage img in images)
            {
                ilThumbnailList.Images.Add(img.Thumbnail);
            }
        }

        public void ClearItems()
        {
            Clear();
            ilThumbnailList.Images.Clear();
        }

        public void changeSizeThumb(System.Drawing.Size newSize)
        {
            ilThumbnailList.ImageSize = newSize;
        }
    }
}
