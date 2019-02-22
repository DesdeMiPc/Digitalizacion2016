using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Digitalizacion2014.Scan.Images.Transforms;

namespace Digitalizacion2014.Scan.Images
{
    public interface IScannedImage : IDisposable
    {
        /// <summary>
        /// Gets a thumbnail bitmap for the image. The consumer should NOT call Dispose on the returned bitmap.
        /// </summary>
        Bitmap Thumbnail { get; }

        /// <summary>
        /// Gets a copy of the scanned image. The consumer is responsible for calling Dispose on the returned bitmap.
        /// </summary>
        /// <returns>A copy of the scanned image.</returns>
        Bitmap GetImage();

        String GetImageBase64();

        /// <summary>
        /// Gets a stream for the scanned image. The consumer is responsible for calling Dispose on the returned stream.
        /// </summary>
        /// <returns>A stream for the scanned image.</returns>
        Stream GetImageStream();

        Object Tag { get; set; }

        /// <summary>
        /// Adds a transform to the image.
        /// </summary>
        /// <param name="transform">The transform.</param>
        void AddTransform(Transform transform);

        /// <summary>
        /// Removes all of the transforms from the image.
        /// </summary>
        void ResetTransforms();

        /// <summary>
        /// Updates the image's thumbnail with all of the transforms.
        /// </summary>
        void UpdateThumbnail();

        /// <summary>
        /// Indicates the the scanned image has been moved to the given position in the scanned image list.
        /// </summary>
        /// <param name="index">The index at which the image was inserted after being removed.</param>
        void MovedTo(int index);
    }
}
