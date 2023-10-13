using Azure;
using DinkToPdf;
using DinkToPdf.Contracts;
using Google.Protobuf.Reflection;
using System;

namespace e_commerce.helpers
{
    public class PDF
    {

        public static byte[] Generate(String viewPdf , IConverter _convert)
        {


            String fileName = "persons.pdf";
            var glb = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings()
                {
                    Bottom = 20,
                    Top = 20,
                    Left = 20,
                    Right = 20
                },
                DocumentTitle = "Person",
                //Out = Path.Combine(Directory.GetCurrentDirectory(), "Exports", fileName)

            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = viewPdf,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
            };

            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = glb,
                Objects = { objectSettings }
            };

            byte[] pdfView = _convert.Convert(pdf);
            return pdfView;
        }

    }
}
