using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Imaging;

namespace Document_Tracking_System__DoTS
{
    public partial class DocumentTrackingQR_PDF : Form
    {
        public DocumentTrackingQR_PDF()
        {
            InitializeComponent();
        }
        /*PrintPreviewDialog printPreiew = new PrintPreviewDialog();
        PrintDocument printDoc = new PrintDocument();*/
        Bitmap panelContents;


        // Create a method to save the panel contents to a PDF file
        public void SaveToPdf(Panel panel, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();
                PdfContentByte cb = writer.DirectContent;

                using (MemoryStream ms = new MemoryStream())
                {
                    panel.DrawToBitmap(new Bitmap(panel.Width, panel.Height), new System.Drawing.Rectangle(0, 0, panel.Width, panel.Height));
                    Bitmap bmp = new Bitmap(ms);
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Bmp);

                    img.ScaleToFit(doc.PageSize.Width - 20, doc.PageSize.Height - 20);
                    img.SetAbsolutePosition((doc.PageSize.Width - img.ScaledWidth) / 2, (doc.PageSize.Height - img.ScaledHeight) / 2);

                    cb.AddImage(img);
                }

                doc.Close();
                writer.Close();
            }
        }

        private void DocumentTrackingQR_PDF_Load(object sender, EventArgs e)
        {
            PrintDocument();

            saveToPDF();

            this.Close();
        }
        private void PrintDocument()
        {
            PrintDocument pd = new PrintDocument();

            pd.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;

                // Get the selected paper size from the PrintDocument
                PaperSize selectedPaperSize = pd.DefaultPageSettings.PaperSize;

                // Set the panel size to match the paper size
                pnl_DocumentQRCode.Size = new Size(selectedPaperSize.Width, selectedPaperSize.Height);

                // Get the panel's contents as an image
                panelContents = new Bitmap(pnl_DocumentQRCode.Width, pnl_DocumentQRCode.Height);
                pnl_DocumentQRCode.DrawToBitmap(panelContents, new System.Drawing.Rectangle(0, 0, pnl_DocumentQRCode.Width, pnl_DocumentQRCode.Height));

                // Draw the image on the printable area
                g.DrawImage(panelContents, e.MarginBounds.Location);
            };

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Start the printing process
                pd.Print();
            }
        }

        private void DocumentTrackingQR_PDF_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void saveToPDF()
        {
            try
            {
                // Specify the directory and file name for the PDF
                string directory = "C:/Leyte Division/Document QR/";
                string fileName = "" + lbl_DocTitle.Text + ".pdf";
                string filePath = Path.Combine(directory, fileName);

                // Save the printed area as a PDF file
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    Document document = new Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    document.Open();

                    // Create an iTextSharp Image object from the panel contents
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(panelContents, ImageFormat.Png);

                    // Set the size of the image to fit the PDF document
                    image.ScaleToFit(document.PageSize.Width, document.PageSize.Height);

                    // Add the image to the PDF document
                    document.Add(image);

                    document.Close();
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDocOwner_Click(object sender, EventArgs e)
        {

        }
    }
    
}
