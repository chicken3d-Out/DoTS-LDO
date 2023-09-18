using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Document_Tracking_System__DoTS
{
    public partial class AddDocument : UserControl
    {
        private Random random;

        string formattedDate;
        string dateReceived;
        string dateExpired;
        int lastInsertedId;

        public AddDocument()
        {
            InitializeComponent();
            random = new Random();
        }
        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        OpenFileDialog openFileDialog = new OpenFileDialog();

        private Bitmap qrCodeBitmap;

        private void btnBackDocument_Click(object sender, EventArgs e)
        {
            this.SendToBack();

        }

        private void SetOverdueDate()
        {
            // Get the current date
            DateTime currentDate = DateTime.Now.Date;

            // Add 15 days to the current date
            DateTime targetDate = currentDate.AddDays(15);

            // Check if the resulting date is a Saturday or Sunday
            while (targetDate.DayOfWeek == DayOfWeek.Saturday || targetDate.DayOfWeek == DayOfWeek.Sunday)
            {
                targetDate = targetDate.AddDays(1); // Increment the date by one day
            }

            // Set the resulting date in the DateTimePicker control
            dateOverdue.Value = targetDate;
        }
        void fillCombo()
        {
            string query = "SELECT * from unit;";
            MySqlCommand data = new MySqlCommand(query, con);
            MySqlDataReader myReader;

            cmb_forwardedTo.Items.Clear();
            /*cmb_DocType.Items.Clear();*/

            try
            {
                //Add Items on AccessLvl
                /*cmb_DocType.Items.AddRange(new string[] { "Division Memorandum", "Travel Order" });*/

                con.Open();
                myReader = data.ExecuteReader();
                while (myReader.Read())
                {
                    string unitName = myReader.GetString("name");
                    cmb_forwardedTo.Items.Add(unitName);
                }
                myReader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelDropFile_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the dragged data contains file(s) with the PDF extension
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);
                    if (extension != null && extension.ToLower() == ".pdf")
                    {
                        // Allow the drop since it's a PDF file
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }

            // Disallow the drop if it's not a PDF file
            e.Effect = DragDropEffects.None;
        }

        private void panelDropFile_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the dropped file(s)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    // Process the file as needed (e.g., save it to a specific location)
                    // Example: File.Copy(file, "destinationPath");

                    // Display the uploaded file name
                    DisplayUploadedFileName(file);
                }
            }
        }
        private void DisplayUploadedFileName(string filePath)
        {
            // Get the file name
            string fileName = Path.GetFileName(filePath);

            // Display the file name
            lblFileName.Text = fileName;
        }

        private void AddDocument_Load(object sender, EventArgs e)
        {
            lblFileName.Anchor = AnchorStyles.None;
            lblFileName.Location = new Point((panel31.Width - lblFileName.Width) / 2, (panel31.Height - lblFileName.Height) / 2);

            //Fill Combo Box
            fillCombo();

            //Generate
            generateTrackingNo();

            SetOverdueDate();

        }
        private void generateTrackingNo()
        {
            //GET DATE TODAY
            // Get today's date
            DateTime today = DateTime.Today;

            // Format the date as "yyyy-MM-dd" (e.g., 2023-05-19)
            formattedDate = today.ToString("yyyy-MM-dd");

            // Remove the hyphens ("-") from the formatted date
            string cleanedDate = formattedDate.Replace("-", "");

            // Get the current time
            DateTime currentTime = DateTime.Now;

            // Format the time as "HH:mm:ss" (e.g., 15:30:45)
            string formattedTime = currentTime.ToString("HH:mm:ss");

            // Remove the colons (":") from the formatted time
            string cleanedTime = formattedTime.Replace(":", "");

            string prefix = "LDO-";

            //Generate Random 5 Letters
            string randomString = GenerateRandomString(7);
            lblTrackingNo.Text = Convert.ToString(prefix + cleanedDate.ToString() + "-" + randomString);
        }

        private string GenerateRandomString(int length)
        {
            string letters = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            char[] randomLetters = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomLetters[i] = letters[random.Next(letters.Length)];
            }

            return new string(randomLetters);
        }

        private void lblUploadFileMessage_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void lblUploadFileMessage_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file(s)
                foreach (string fileName in openFileDialog.FileNames)
                {
                    DisplayUploadedFileName(fileName);
                }
            }
        }

        private void btnGenerateQRCode_Click(object sender, EventArgs e)
        {
            generateQRCode();
        }

        private void generateQRCode()
        {
            //Data For Database
            // Get today's date
            DateTime todays = DateTime.Today;

            // Format the date as "yyyy-MM-dd" (e.g., 2023-05-19)
            dateReceived = todays.ToString("yyyy-MM-dd");

            //Get Value From Date Expired
            dateExpired = dateOverdue.Value.ToString("yyyy-MM-dd");

            // Define the information to be encoded in the QR code
            string trackingNo = Convert.ToString(lblTrackingNo.Text);
            string docTitle = txt_DocTitle.Text;
            string forwardedTo = cmb_forwardedTo.Text;
            string dateReceivedDoc = dateReceived.ToString();
            string docType = cmb_DocType.Text;
            string overdueDate = dateExpired.ToString();
            //
            string name = cmb_docOwnerName.Text;
            string mobileNo = txt_contactNum.Text;
            string agency = txt_Agency.Text;
            string email = txt_email.Text;

            // Create a dictionary to hold the data
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["trackingNo"] = trackingNo.ToString();
            data["docTitle"] = docTitle;
            data["forwardedTo"] = forwardedTo;
            data["dateReceivedDoc"] = dateReceivedDoc;
            data["docType"] = docType;
            data["overdueDate"] = overdueDate;

            data["id"] = lastInsertedId.ToString();
            data["name"] = name;
            data["mobileNo"] = mobileNo;
            data["agency"] = agency;
            data["email"] = email;


            // Convert the data to JSON
            string jsonData = JsonConvert.SerializeObject(data);

            //
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 478, // Adjust the width to your desired size
                Height = 445, // Adjust the height to your desired size
                Margin = 0
            };

            // Set up the QR code writer
            BarcodeWriter barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };
            

            // Generate the QR code bitmap
            qrCodeBitmap = new Bitmap(barcodeWriter.Write(jsonData));


            // Create a new bitmap with transparent background
            /*Bitmap transparentBitmap = new Bitmap(qrCodeBitmap.Width, qrCodeBitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Draw the QR code onto the transparent bitmap
            using (Graphics g = Graphics.FromImage(transparentBitmap))
            {
                g.Clear(Color.Transparent);
                g.DrawImage(qrCodeBitmap, 0, 0);
            }*/

            // Display the QR code bitmap in the PictureBox control
            /*QRCode.Image = qrCodeBitmap;*/

            string logoFilePath = "C:/Leyte Division/Logo/DIVISION LOGO.png";
            int logoWidth = 100;
            int logoHeight = 100;

            Bitmap qrCodeWithLogo = AddLogoToQRCode(qrCodeBitmap, logoFilePath, logoWidth, logoHeight);

            QRCode.Image = qrCodeWithLogo;

        }

        private Bitmap AddLogoToQRCode(Bitmap qrCode, string logoFilePath, int logoWidth, int logoHeight)
        {
            var logoImage = new Bitmap(logoFilePath);

            // Resize the logo image to fit within the QR code
            logoImage = new Bitmap(logoImage, new Size(logoWidth, logoHeight));

            var graphics = Graphics.FromImage(qrCode);
            var xPos = (qrCode.Width - logoImage.Width) / 2;
            var yPos = (qrCode.Height - logoImage.Height) / 2;

            graphics.DrawImage(logoImage, xPos, yPos, logoImage.Width, logoImage.Height);
            graphics.Flush();

            return qrCode;
        }

        private void btn_Publish_Click(object sender, EventArgs e)
        {
            // Prompt for validation before closing the form
            DialogResult result = MessageBox.Show("Check All Details are Correct", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                //Generate QR Code
                try
                {
                    if (txt_DocTitle.Text == "")
                    {
                        MessageBox.Show("Document Title is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_DocType.Text == "")
                    {
                        MessageBox.Show("Document Type Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (dateOverdue.Text == "")
                    {
                        MessageBox.Show("Overdue Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_forwardedTo.Text == "")
                    {
                        MessageBox.Show("Forwarded To Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_OwnerType.Text == "")
                    {
                        MessageBox.Show("Forwarded To Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_docOwnerName.Text == "")
                    {
                        MessageBox.Show("Forwarded To Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_forwardedTo.Text == "")
                    {
                        MessageBox.Show("Forwarded To Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (rtxt_Description.Text == "")
                    {
                        MessageBox.Show("Document Description Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if ((openFileDialog.FileName).Length == 0)
                    {
                        MessageBox.Show("Please Upload the Scanned Document!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else if (QRCode.Image == null)
                    {
                        MessageBox.Show("QR Code is Not Generated!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Get the filename and store in a specific directory
                        string scannedDoc = Convert.ToString(txt_DocTitle.Text + ".pdf");
                        System.IO.File.Copy(openFileDialog.FileName, @"C:\Leyte Division\Scanned Document\" + scannedDoc);


                        //Date Today
                        // Get today's date
                        DateTime today = DateTime.Today;

                        // Format the date as "yyyy-MM-dd" (e.g., 2023-05-19)
                        dateReceived = today.ToString("yyyy-MM-dd");

                        //Get Value From Date Expired
                        dateExpired = dateOverdue.Value.ToString("yyyy-MM-dd");

                        if(lblOwnerType.Text == "Unit")
                        {
                            //OPEN CONNECTION FOR MYSQL QUERY
                            con.Open();
                            string addDocumentsUnit = "INSERT INTO documents (documentID,docOwnerID,trackingNo,docTitle,documentType,forwardedTo,scannedDoc,dateReceived,dateReleased,dateExpired,description,status) VALUES ('',"+lblIDOwner.Text+",'" + lblTrackingNo.Text + "','" + txt_DocTitle.Text + "','" + cmb_DocType.Text + "','" + lblUnitID.Text + "','" + scannedDoc + "','"+ dateReceived.ToString() +"','N/A','"+dateExpired.ToString()+"','"+rtxt_Description.Text+"','Processing');";
                            cmd = new MySqlCommand(addDocumentsUnit, con);
                            int success1 = cmd.ExecuteNonQuery();
                            con.Close();

                            if (success1 > 0)
                            {
                                MessageBox.Show("Document Successfully Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                getLastInsertedID();
                                
                                generateQRCode();
                                CreateTrackingDocfile();
                                generateTrackingNo();

                            }
                            else
                            {
                                MessageBox.Show("Failed To Create Document!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                con.Close();
                            }

                        }
                        else if(lblOwnerType.Text == "Agency")
                        {
                            //OPEN CONNECTION FOR MYSQL QUERY
                            con.Open();
                            string addDocumentsAgency = "INSERT INTO documents (documentID,docAgencyID,trackingNo,docTitle,documentType,forwardedTo,scannedDoc,dateReceived,dateReleased,dateExpired,description,status) VALUES (''," + lblIDOwner.Text + ",'" + lblTrackingNo.Text + "','" + txt_DocTitle.Text + "','" + cmb_DocType.Text + "','" + lblUnitID.Text + "','" + scannedDoc + "','" + dateReceived.ToString() + "','N/A','" + dateExpired.ToString() + "','" + rtxt_Description.Text + "','Processing');";
                            cmd = new MySqlCommand(addDocumentsAgency, con);
                            int success1 = cmd.ExecuteNonQuery();
                            con.Close();

                            if (success1 > 0)
                            {
                                MessageBox.Show("Document Successfully Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                getLastInsertedID();
                                
                                
                                generateQRCode();
                                CreateTrackingDocfile();

                                generateTrackingNo();
                            }
                            else
                            {
                                MessageBox.Show("Failed To Create Document!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                con.Close();
                            }

                        }
                        else if (lblOwnerType.Text == "Others")
                        {
                            //OPEN CONNECTION FOR MYSQL QUERY
                            con.Open();
                            string addDocumentsOthers = "INSERT INTO documents (documentID,docOthersID,trackingNo,docTitle,documentType,forwardedTo,scannedDoc,dateReceived,dateReleased,dateExpired,description,status) VALUES (''," + lblIDOwner.Text + ",'" + lblTrackingNo.Text + "','" + txt_DocTitle.Text + "','" + cmb_DocType.Text + "','" + lblUnitID.Text + "','" + scannedDoc + "','" + dateReceived.ToString() + "','N/A','" + dateExpired.ToString() + "','" + rtxt_Description.Text + "','Processing');";
                            cmd = new MySqlCommand(addDocumentsOthers, con);
                            int success1 = cmd.ExecuteNonQuery();
                            con.Close();

                            if (success1 > 0)
                            {
                                MessageBox.Show("Document Successfully Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                getLastInsertedID();
                                
                                
                                generateQRCode();
                                CreateTrackingDocfile();

                                generateTrackingNo();
                            }
                            else
                            {
                                MessageBox.Show("Failed To Create Document!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                con.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Message: " + ex.Message);
                }
            }        
        }

        public void getLastInsertedID()
        {
            con.Open();

            // Get the last inserted ID
            string getLastInsertedIdQuery = "SELECT LAST_INSERT_ID()";
            MySqlCommand getLastInsertedIdCommand = new MySqlCommand(getLastInsertedIdQuery, con);
            lastInsertedId = Convert.ToInt32(getLastInsertedIdCommand.ExecuteScalar());
            con.Close();
        }

        public void CreateTrackingDocfile()
        {
            DocumentTrackingQR_PDF updatePDF = new DocumentTrackingQR_PDF();
            updatePDF.lbl_TrackingNo.Text = lblTrackingNo.Text;
            updatePDF.lblDocOwner.Text = cmb_docOwnerName.Text;
            updatePDF.lblReceiver.Text = cmb_forwardedTo.Text;
            updatePDF.lbl_DocTitle.Text = txt_DocTitle.Text;
            updatePDF.lbl_DocType.Text = cmb_DocType.Text;

            updatePDF.id.Text = lastInsertedId.ToString();

            // Check if pictureBox1 has an image
            if (qrCodeBitmap != null)
            {
                // Assign the image to pictureBox2
                updatePDF.pb_QRCode.Image = qrCodeBitmap;

            }

            updatePDF.ShowDialog();
        }

        private void cmb_OwnerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIDOwner.Text = "";
            cmb_docOwnerName.Text = "";
            txt_Address.Text = "";
            txt_Agency.Text = "";
            txt_email.Text = "";
            txt_contactNum.Text = "";

            lblOwnerType.Text = Convert.ToString(cmb_OwnerType.GetItemText(cmb_OwnerType.SelectedItem));

            if (lblOwnerType.Text == "Unit")
            {
                con.Open();
                string query = "SELECT * from docowner;";
                MySqlCommand data = new MySqlCommand(query, con);
                MySqlDataReader my;

                cmb_docOwnerName.Items.Clear();

                try
                {

                    my = data.ExecuteReader();
                    while (my.Read())
                    {
                        string ownerName = my.GetString("name");
                        cmb_docOwnerName.Items.Add(ownerName);
                    }
                    /*myReader.Close();*/
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (lblOwnerType.Text == "Agency")
            {
                con.Open();
                string query = "SELECT * from docowneragency;";
                MySqlCommand data = new MySqlCommand(query, con);
                MySqlDataReader myR;

                cmb_docOwnerName.Items.Clear();

                try
                {

                    myR = data.ExecuteReader();
                    while (myR.Read())
                    {
                        string ownerName = myR.GetString("name");
                        cmb_docOwnerName.Items.Add(ownerName);
                    }
                    /*myReader.Close();*/
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (lblOwnerType.Text == "Others")
            {
                con.Open();
                string query = "SELECT * from docownerothers;";
                MySqlCommand data = new MySqlCommand(query, con);
                MySqlDataReader myRe;

                cmb_docOwnerName.Items.Clear();

                try
                {

                    myRe = data.ExecuteReader();
                    while (myRe.Read())
                    {
                        string ownerName = myRe.GetString("name");
                        cmb_docOwnerName.Items.Add(ownerName);
                    }
                    /*myReader.Close();*/
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void cmb_docOwnerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get the selected name
            string docOwner = cmb_docOwnerName.GetItemText(cmb_docOwnerName.SelectedItem);

            if (lblOwnerType.Text == "Unit")
            {
                con.Open();

                string query = "SELECT docOwnerID from docowner where name ='" + docOwner + "';";
                MySqlCommand data = new MySqlCommand(query, con);
                MySqlDataReader myReader;


                myReader = data.ExecuteReader();
                while (myReader.Read())
                {
                    lblIDOwner.Text = myReader.GetInt32("docOwnerID").ToString();
                }
                myReader.Close();
                con.Close();


                if (Convert.ToInt32(lblIDOwner.Text) != 0)
                {
                    //Fill Up Forms
                    con.Open();

                    string query1 = "select unit.name as 'agency', unit.email as 'email', unit.phoneNum as 'phoneNo', unit.located as 'address' from unit, docowner where docowner.agency = unit.unitID and docowner.docOwnerID =" + lblIDOwner.Text + ";";
                    MySqlCommand data1 = new MySqlCommand(query1, con);
                    MySqlDataReader myReader1;

                    myReader1 = data1.ExecuteReader();
                    while (myReader1.Read())
                    {
                        txt_Agency.Text = myReader1.GetString("agency").ToString();
                        txt_Address.Text = myReader1.GetString("address").ToString();
                        txt_contactNum.Text = myReader1.GetString("phoneNo").ToString();
                        txt_email.Text = myReader1.GetString("email").ToString();
                    }
                    myReader1.Close();
                    con.Close();
                }

            }
            else if (lblOwnerType.Text == "Agency")
            {
                con.Open();

                string query = "SELECT docagencyID from docowneragency where name ='" + docOwner + "';";
                MySqlCommand data = new MySqlCommand(query, con);
                MySqlDataReader myReader;


                myReader = data.ExecuteReader();
                while (myReader.Read())
                {
                    lblIDOwner.Text = myReader.GetInt32("docagencyID").ToString();
                }
                myReader.Close();
                con.Close();


                if (Convert.ToInt32(lblIDOwner.Text) != 0)
                {
                    //Fill Up Forms
                    con.Open();

                    string query1 = "select * from docowneragency where docagencyID = "+lblIDOwner.Text+";";
                    MySqlCommand data1 = new MySqlCommand(query1, con);
                    MySqlDataReader myReader1;

                    myReader1 = data1.ExecuteReader();
                    while (myReader1.Read())
                    {
                        txt_Agency.Text = myReader1.GetString("agencyName").ToString();
                        txt_Address.Text = myReader1.GetString("address").ToString();
                        txt_contactNum.Text = myReader1.GetString("contactNo").ToString();
                        txt_email.Text = myReader1.GetString("email").ToString();
                    }
                    myReader1.Close();
                    con.Close();
                }

            }
            else if (lblOwnerType.Text == "Others")
            {
                con.Open();

                string query = "SELECT docothersID from docownerothers where name ='" + docOwner + "';";
                MySqlCommand data = new MySqlCommand(query, con);
                MySqlDataReader myReader;


                myReader = data.ExecuteReader();
                while (myReader.Read())
                {
                    lblIDOwner.Text = myReader.GetInt32("docothersID").ToString();
                }
                myReader.Close();
                con.Close();


                if (Convert.ToInt32(lblIDOwner.Text) != 0)
                {
                    //Fill Up Forms
                    con.Open();

                    string query1 = "select * from docownerothers where docothersID = " + lblIDOwner.Text + ";";
                    MySqlCommand data1 = new MySqlCommand(query1, con);
                    MySqlDataReader myReader1;

                    myReader1 = data1.ExecuteReader();
                    while (myReader1.Read())
                    {
                        txt_Agency.Text = myReader1.GetString("agencyName").ToString();
                        txt_Address.Text = myReader1.GetString("address").ToString();
                        txt_contactNum.Text = myReader1.GetString("contactNo").ToString();
                        txt_email.Text = myReader1.GetString("email").ToString();
                    }
                    myReader1.Close();
                    con.Close();
                }
            }
        }

        private void cmb_forwardedTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unit = cmb_forwardedTo.GetItemText(cmb_forwardedTo.SelectedItem);

            string quer = "SELECT unitID from unit where Name ='" + unit + "';";
            MySqlCommand deta = new MySqlCommand(quer, con);
            MySqlDataReader myRead;

            con.Open();
            myRead = deta.ExecuteReader();
            while (myRead.Read())
            {
                lblUnitID.Text = myRead.GetInt32("unitID").ToString();
            }
            con.Close();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            //Generate QR Code
            generateTrackingNo();
            

        }
        public void CreatePDFDocs()
        {


            /*// Create a new document with A4 page size
            Document document = new Document(PageSize.A4);

            // Set the file path for the PDF file
            string filePath = "C:\\PDFfile\\file.pdf";

            // Create a new instance of PdfWriter to write the document to a file
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            // Open the PDF document
            document.Open();

            // Add logo
            string logoPath = "C:\\Logo\\logo.png";
            if (File.Exists(logoPath))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.Alignment = Element.ALIGN_CENTER;
                logo.ScaleAbsolute(50, 50);
                document.Add(logo);
            }

            // Add header
            Paragraph header = new Paragraph("Republic of the Philippines \n Department of Education \n REGION VIII \n Schools Division of Leyte ");
            header.Alignment = Element.ALIGN_CENTER;
            

            document.Add(header);

            

            // Add content
            document.Add(new Paragraph("Hello, World!"));

            // Close the PDF document
            document.Close();

            // Preview the PDF file
            System.Diagnostics.Process.Start(filePath);*/
        }
    }
}
