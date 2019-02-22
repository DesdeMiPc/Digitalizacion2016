using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digitalizacion2014.Scan.Images;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

namespace Digitalizacion2014.ImportExport.Images
{
    public class ImageSaver
    {
        private readonly ImageFileNamer imageFileNamer;
        private readonly IErrorOutput errorOutput;

        public ImageSaver(ImageFileNamer imageFileNamer, IErrorOutput errorOutput)
        {
            this.imageFileNamer = imageFileNamer;
            this.errorOutput = errorOutput;
        }

        /// <summary>
        /// Saves the provided collection of images to a file with the given name. The image type is inferred from the file extension.
        /// If multiple images are provided, they will be saved to files with numeric identifiers, e.g. img1.jpg, img2.jpg, etc..
        /// </summary>
        /// <param name="fileName">The name of the file to save. For multiple images, this is modified by appending a number before the extension.</param>
        /// <param name="images">The collection of images to save.</param>
        /// <param name="overwritePredicate">A predicate that, given the full name/path of a file that already exists, returns true if it should be overwritten, or false if it should be skipped.</param>
        public void SaveImages(string fileName, ICollection<IScannedImage> images, Func<string, bool> overwritePredicate)
        {
            try
            {
                ImageFormat format = GetImageFormat(fileName);

                //if (Equals(format, ImageFormat.Tiff))
                //{
                //    if (File.Exists(fileName))
                //    {
                //        // Overwrite?
                //        if (!overwritePredicate(Path.GetFullPath(fileName)))
                //        {
                //            // No, so skip it
                //            return;
                //        }
                //    }
                //    Image[] bitmaps = images.Select(x => (Image)x.GetImage()).ToArray();
                //    TiffHelper.SaveMultipage(bitmaps, fileName);
                //    foreach (Image bitmap in bitmaps)
                //    {
                //        bitmap.Dispose();
                //    }
                //    return;
                //}

                var fileNames = imageFileNamer.GetFileNames(fileName, images.Count).GetEnumerator();
                foreach (IScannedImage img in images)
                {
                    using (Bitmap baseImage = img.GetImage())
                    {
                        fileNames.MoveNext();
                        if (File.Exists(fileNames.Current))
                        {
                            // Overwrite?
                            if (!overwritePredicate(Path.GetFullPath(fileNames.Current)))
                            {
                                // No, so skip it
                                continue;
                            }
                        }
                        baseImage.Save(fileNames.Current, format);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                errorOutput.DisplayError("No tienes permisos para guardar archivos en esa ubicación.");
            }
        }

        private static ImageFormat GetImageFormat(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            Debug.Assert(extension != null);
            switch (extension.ToLower())
            {
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".emf":
                    return ImageFormat.Emf;
                case ".gif":
                    return ImageFormat.Gif;
                case ".ico":
                    return ImageFormat.Icon;
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                //case ".tif":
                //case ".tiff":
                //    return ImageFormat.Tiff;
                case ".wmf":
                    return ImageFormat.Wmf;
                default:
                    return ImageFormat.Jpeg;
            }
        }
    }
}
