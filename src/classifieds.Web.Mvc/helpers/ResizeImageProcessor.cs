using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using SixLabors.ImageSharp.Web;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.Processors;

namespace classifieds.Web.helpers
{
    /// <summary>
    /// Allows the resizing of images.
    /// </summary>
    public class ResizeWebProcessor : IImageWebProcessor
    {
        public IEnumerable<string> Commands => throw new NotImplementedException();

        public FormattedImage Process(FormattedImage image, ILogger logger, CommandCollection commands, CommandParser parser, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool RequiresTrueColorPixelFormat(CommandCollection commands, CommandParser parser, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
