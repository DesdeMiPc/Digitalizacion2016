using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Digitalizacion2014.Scan.Images.Transforms;

namespace Digitalizacion2014.Scan.Images
{
    public class ScannedImage : IScannedImage
    {
        // The image's bit depth (or C24Bit if unknown)
        private readonly ScanBitDepth bitDepth;
        // Only one of the following (baseImage/baseImageEncoded) should have a value for any particular ScannedImage
        private readonly Bitmap baseImage;
        private readonly MemoryStream baseImageEncoded;
        private readonly ImageFormat baseImageFileFormat;
        // Store a base image and transform pair (rather than doing the actual transform on the base image)
        // so that JPEG degradation is minimized when multiple rotations/flips are performed
        private readonly List<Transform> transformList = new List<Transform>();

        public ScannedImage(Bitmap img, ScanBitDepth bitDepth, bool highQuality)
        {
            this.bitDepth = bitDepth;
            Thumbnail = ThumbnailHelper.GetThumbnail(img);
            ScannedImageHelper.GetSmallestBitmap(img, bitDepth, highQuality, out baseImage, out baseImageEncoded, out baseImageFileFormat);
        }

        public ScannedImage(String imgBase64, ScanBitDepth bitDepth, bool highQuality)
        {

            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(imgBase64)))
            using (Image image = Image.FromStream(stream))
            {
                this.bitDepth = bitDepth;
                Thumbnail = ThumbnailHelper.GetThumbnail(new Bitmap(image));
                ScannedImageHelper.GetSmallestBitmap(new Bitmap(image), bitDepth, highQuality, out baseImage, out baseImageEncoded, out baseImageFileFormat);
            }

            ////Convertir String a Imagen
            //// Convert Base64 String to byte[]
            //byte[] imageBytes = Convert.FromBase64String(imgBase64);
            //// Convert byte[] to Image
            //using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            //{
            //    Image image = Image.FromStream(ms, true);
            //
            //
            //
            //}
        }

        public Bitmap Thumbnail { get; private set; }

        public Bitmap GetImage()
        {
            var bitmap = bitDepth == ScanBitDepth.BlackWhite ? (Bitmap)baseImage.Clone() : new Bitmap(baseImageEncoded);
            return Transform.PerformAll(bitmap, transformList);
        }

        public Stream GetImageStream()
        {
            using (var transformed = GetImage())
            {
                var stream = new MemoryStream();
                transformed.Save(stream, baseImageFileFormat);
                return stream;
            }
        }

        public String GetImageBase64()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                //GetImage().Save(stream, GetImage().RawFormat);
                GetImage().Save(stream, ImageFormat.Jpeg);
                return Convert.ToBase64String(stream.ToArray());
            }

            //Bitmap bImage = GetImage();  //Your Bitmap Image
            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //bImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] byteImage = ms.ToArray();
            //return Convert.ToBase64String(byteImage); //Get Base64
        }

        public Object Tag { get; set; }

        public void Dispose()
        {
            if (baseImage != null)
            {
                baseImage.Dispose();
            }
            if (baseImageEncoded != null)
            {
                baseImageEncoded.Dispose();
            }
            Thumbnail.Dispose();
        }

        public void AddTransform(Transform transform)
        {
            Transform.AddOrSimplify(transformList, transform);
        }

        public void ResetTransforms()
        {
            transformList.Clear();
        }

        public void UpdateThumbnail()
        {
            using (var img = GetImage())
            {
                Thumbnail = ThumbnailHelper.GetThumbnail(img);
            }
        }

        public void MovedTo(int index)
        {
            // Do nothing, this is only important for FileBasedScannedImage
        }
    }
}

