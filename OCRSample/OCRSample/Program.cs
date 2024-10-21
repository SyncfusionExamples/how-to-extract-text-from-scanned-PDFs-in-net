using Syncfusion.Pdf.Parsing;
using Syncfusion.OCRProcessor;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
namespace OCRSample {
    internal class Program {
        static void Main(string[] args) {

            //Initialize the OCR processor.
            using (OCRProcessor processor = new OCRProcessor()) {
                //Load an existing PDF document.
                FileStream stream = new FileStream(Path.GetFullPath(@"../../../Input.pdf"), FileMode.Open);
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(stream);
                //Set OCR language.
                processor.Settings.Language = Languages.English;
                //Perform OCR with input document.
                processor.PerformOCR(loadedDocument);

                //Create file stream.
                using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                    //Save the PDF document to file stream.
                    loadedDocument.Save(outputFileStream);
                }
                //Close the document.
                loadedDocument.Close(true);
            }

            PerformOCR_Particular_Region();
            OCR_Image_to_PDF();
            PerformOCR_Image();
        }
        static void PerformOCR_Particular_Region() {
            //Initialize the OCR processor.
            using (OCRProcessor processor = new OCRProcessor()) {
                //Load an existing PDF document.
                FileStream stream = new FileStream(Path.GetFullPath(@"../../../Input.pdf"), FileMode.Open);
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(stream);
                //Set OCR language.
                processor.Settings.Language = Languages.English;
                //Assign rectangles to the page.
                RectangleF rect = new RectangleF(0, 100, 950, 150);
                List<PageRegion> pageRegions = new List<PageRegion>();
                //Create page region.
                PageRegion region = new PageRegion();
                //Set page index.
                region.PageIndex = 0;
                //Set the page region.
                region.PageRegions = new RectangleF[] { rect };
                //Add region to page region.
                pageRegions.Add(region);
                //Set the regions to process OCR.
                processor.Settings.Regions = pageRegions;
                //Perform OCR with input document.
                processor.PerformOCR(loadedDocument);

                //Create file stream.
                using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                    //Save the PDF document to file stream.
                    loadedDocument.Save(outputFileStream);
                }
                //Close the document.
                loadedDocument.Close(true);
            }
        }
        static void OCR_Image_to_PDF() {
            //Initialize the OCR processor.
            using (OCRProcessor processor = new OCRProcessor()) {
                //Load the input image.
                FileStream stream = new FileStream(Path.GetFullPath(@"../../../Input.jpg"), FileMode.Open);
                //Set OCR language.
                processor.Settings.Language = Languages.English;
                //Sets Unicode font to preserve the Unicode characters in a PDF document.
                FileStream fontStream = new FileStream(Path.GetFullPath(@"../../../ARIALUNI.ttf"), FileMode.Open);
                //Set the Unicode font. 
                processor.UnicodeFont = new PdfTrueTypeFont(fontStream, true, PdfFontStyle.Regular, 10);
                //Set the PDF conformance level.
                processor.Settings.Conformance = PdfConformanceLevel.Pdf_A1B;
                //Process OCR by providing the image. 
                PdfDocument document = processor.PerformOCR(stream);

                //Create file stream.
                using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);
                }
                //Close the document.
                document.Close(true);
            }
        }
        static void PerformOCR_Image() {
            //Initialize the OCR processor.
            using (OCRProcessor processor = new OCRProcessor()) {
                //Get stream from the image file.
                FileStream stream = new FileStream(Path.GetFullPath(@"../../../Input.jpg"), FileMode.Open);
                //Set OCR language to process.
                processor.Settings.Language = Languages.English;
                //Perform the OCR process for an image steam.
                string OCRText = processor.PerformOCR(stream, processor.TessDataPath);

                //Write the OCR'ed text in text file.
                using (StreamWriter writer = new StreamWriter("OCRText.txt", true)) {
                    writer.WriteLine(OCRText);
                }
            }
        }
    }
}