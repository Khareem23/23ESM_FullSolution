using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ZXing;
using ZXing.Common;

namespace KaylaaShop.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("barcode")]
    public class BarcodeTagHelper : TagHelper
    {

        public enum OutputFormat
        {
            PNG,
            SVG
        }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var content = context.AllAttributes["content"]?.Value.ToString();

            if (String.IsNullOrEmpty(content))
                return;

            var alt = context.AllAttributes["alt"]?.Value.ToString();
            var width =
               Convert.ToInt32(String.IsNullOrEmpty(context.AllAttributes["width"]?.Value.ToString())
                  ? "1"
                  : context.AllAttributes["width"].Value.ToString());
            var height =
               Convert.ToInt32(String.IsNullOrEmpty(context.AllAttributes["height"]?.Value.ToString())
                  ? "1"
                  : context.AllAttributes["height"].Value.ToString());
            var margin =
               Convert.ToInt32(String.IsNullOrEmpty(context.AllAttributes["margin"]?.Value.ToString())
                  ? "0"
                  : context.AllAttributes["margin"].Value.ToString());
            var barcodeformat = BarcodeFormat.CODE_128;
            var outputformat = OutputFormat.PNG;

            if (context.AllAttributes["barcodeformat"] != null)
            {
                if (!Enum.TryParse(context.AllAttributes["barcodeformat"].Value.ToString(), true, out barcodeformat))
                {
                    barcodeformat = BarcodeFormat.CODE_128;
                }
            }

            if (context.AllAttributes["outputformat"] != null)
            {
                if (!Enum.TryParse(context.AllAttributes["outputformat"].Value.ToString(), true, out outputformat))
                {
                    outputformat = OutputFormat.PNG;
                }
            }

            switch (outputformat)
            {
                case OutputFormat.PNG:
                    GeneratePng(output, content, barcodeformat, width, height, margin, alt);
                    break;
                case OutputFormat.SVG:
                    GenerateSvg(output, content, barcodeformat, width, height, margin, alt);
                    break;
            }
        }

        private void GeneratePng(TagHelperOutput output, string content, BarcodeFormat barcodeformat, int width, int height, int margin, string alt)
        {
            var qrWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = barcodeformat,
                Options = new EncodingOptions { Height = height, Width = width, Margin = margin }
            };


            var pixelData = qrWriter.Write(content);

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB
            // the System.Drawing.Bitmap class is provided by the CoreCompat.System.Drawing package
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                // lock the data area for fast access
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                   System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                       pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                output.TagName = "img";
                output.Attributes.Clear();
                output.Attributes.Add("id", "barcodeImg");
                output.Attributes.Add("width", pixelData.Width);
                output.Attributes.Add("height", pixelData.Height);
                output.Attributes.Add("alt", alt);
                output.Attributes.Add("src",
                   String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
            }
        }


        private void GenerateSvg(TagHelperOutput output, string content, BarcodeFormat barcodeformat, int width, int height, int margin, string alt)
        {
            var qrWriter = new ZXing.BarcodeWriterSvg
            {
                Format = barcodeformat,
                Options = new EncodingOptions { Height = height, Width = width, Margin = margin }
            };

            

            var svgImage = qrWriter.Write(content);

            output.TagName = "img";
            output.Attributes.Clear();
            
            output.Attributes.Add("width", svgImage.Width);
            output.Attributes.Add("height", svgImage.Height);
            output.Attributes.Add("alt", alt);
            output.Attributes.Add("src", new Microsoft.AspNetCore.Html.HtmlString(
               String.Format("data:image/svg+xml;{0}", svgImage.Content)));
        }
    }
}
