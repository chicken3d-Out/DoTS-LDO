using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Document_Tracking_System__DoTS
{
    public partial class ScanQR : Form
    {
        public ScanQR()
        {
            InitializeComponent();

            lblStatus.Text = "Pending";
            lblStatus.ForeColor = Color.Orange;
        }
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private BarcodeReader reader;
        private Rectangle qrCodeRegion;
        private ResultPoint[] qrCodePoints;

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        int unitID;

        private void fillComboBox()
        {
            if(lblUsertype.Text == "Admin")
            {
                try
                {
                    cmb_Status.Items.Clear();

                    cmb_Status.Items.AddRange(new string[] { "Forwarded", "Archived", "Returned","Released" });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }else
            {
                try
                {
                    cmb_Status.Items.Clear();

                    cmb_Status.Items.AddRange(new string[] { "Forwarded", "Archived", "Returned"});
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
        }
        private void ScanQR_Load(object sender, EventArgs e)
        {
            try
            {
                fillComboBox();

                getUnitID();

                time();

                //Avoid lagging when form is closed
                this.FormClosing += ScanQR_FormClosing;

                // Initialize and start the camera
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count > 0)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                    videoSource.NewFrame += VideoSource_NewFrame;

                    videoSource.Start();
                }
                else
                {
                    MessageBox.Show("No video devices found.", "Error");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void time()
        {
            Timer timer = new Timer();
            timer.Interval = 60000; // 1 minute = 60,000 milliseconds
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private async void ScanQR_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Prompt for validation before closing the form
                DialogResult result = MessageBox.Show("Are you sure you want to close this form?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancel the form closing event
                    return;
                }
                else
                {
                    // Stop the video source on a separate thread
                    if (videoSource != null && videoSource.IsRunning)
                    {
                        // Stop the video source on a separate thread
                        await Task.Run(() =>
                        {
                            videoSource.SignalToStop();
                            videoSource.WaitForStop();
                        });
                    }
                }
            }catch(Exception ex)
            {

            }
        }
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Initialize the QR code reader
            reader = new BarcodeReader();

            // Get the current video frame from the camera
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();


            // Search for the QR code in the video frame
            Result result = reader.Decode(bitmap);

            if (result != null)
            {
                // Get the position and size of the QR code in the video frame
                qrCodePoints = result.ResultPoints;
                qrCodeRegion = CalculateBoundingBox(qrCodePoints);


                //display
                if (lblContent.InvokeRequired)
                {
                    lblContent.Invoke(new Action(() =>
                    {
                        try
                        {
                            dynamic deserializedObject = JsonConvert.DeserializeObject(result.ToString());

                            /*JsonSerializerSettings settings = new JsonSerializerSettings
                            {
                                FloatParseHandling = FloatParseHandling.Decimal
                            };

                            dynamic deserializedObject = JsonConvert.DeserializeObject(result.ToString(), settings);*/
                            txtTrackingNum.Text = Convert.ToString(deserializedObject.trackingNo);
                            txtDateReceived.Text = deserializedObject.dateReceivedDoc;
                            txtDocTitle.Text = deserializedObject.docTitle;
                            txtDocType.Text = deserializedObject.docType;
                            txtForwardedTo.Text = deserializedObject.forwardedTo;
                            txtOverdue.Text = deserializedObject.overdueDate;

                            txt_Name.Text = deserializedObject.name;
                            txt_MobileNo.Text = deserializedObject.mobileNo;
                            txt_Agency.Text = deserializedObject.agency;
                            txt_Email.Text = deserializedObject.email;

                            lbl_DocID.Text = deserializedObject.id;

                            
                            /*autoReceive();*/
                            autoReceive();
                        }
                        catch (Exception ex)
                        {

                        }

                    }));
                }
                else
                {


                }
            }
            else
            {
                qrCodeRegion = Rectangle.Empty;
                qrCodePoints = null;
            }

            // Draw a rectangle around the QR code region in the video frame
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                if (!qrCodeRegion.IsEmpty)
                {
                    using (Pen pen = new Pen(Color.FromArgb(245, 234, 20), 5))
                    {
                        g.DrawRectangle(pen, qrCodeRegion);
                    }

                    if (qrCodePoints != null)
                    {
                        foreach (ResultPoint point in qrCodePoints)
                        {
                            g.FillEllipse(Brushes.Yellow, point.X - 5, point.Y - 5, 12, 12);
                        }
                    }
                }
            }

            // Display the video frame in the PictureBox control
            scanQRFrame.Invoke(new MethodInvoker(delegate { scanQRFrame.Image = bitmap; }));


            /*try
            {
                // Convert the camera frame to a bitmap
                Bitmap cameraFrame = (Bitmap)eventArgs.Frame.Clone();

                // Create a separate copy of the bitmap
                Bitmap clonedFrame = new Bitmap(cameraFrame);

                // Display the cloned bitmap in the PictureBox control
                if (scanQRFrame.InvokeRequired)
                {
                    scanQRFrame.Invoke(new MethodInvoker(delegate
                    {
                        scanQRFrame.Image = clonedFrame;
                    }));
                }
                else
                {
                    scanQRFrame.Image = clonedFrame;
                }

                // Decode the QR code
                DecodeQRCode(cameraFrame);
            }
            catch
            {
                // Error occurred while accessing the camera frame
            }*/
        }
        private Rectangle CalculateBoundingBox(ResultPoint[] points)
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (ResultPoint point in points)
            {
                if (point.X < minX)
                    minX = (int)point.X;
                if (point.X > maxX)
                    maxX = (int)point.X;
                if (point.Y < minY)
                    minY = (int)point.Y;
                if (point.Y > maxY)
                    maxY = (int)point.Y;
            }

            int width = maxX - minX;
            int height = maxY - minY;

            return new Rectangle(minX, minY, width, height);
        }

        public void autoReceive()
        {
            try
            {
                if (lblStatus.Text == "Pending")
                {
                    // Get today's date
                    DateTime today = DateTime.Today;

                    // Format the date as "yyyy-MM-dd" (e.g., 2023-05-19)
                    string dateToday = today.ToString("yyyy-MM-dd");

                    // Get the current time
                    DateTime currentTime = DateTime.Now;

                    // Format the time as "HH:mm:ss" (e.g., 15:30:45)
                    string timeToday = currentTime.ToString("HH:mm:ss");

                    con.Open();
                    string receivedDoc = "INSERT INTO tracking VALUES (''," + unitID + ",'Received by: " + lblUnitName.Text + " Date: " + dateToday.ToString() + " Time: " + timeToday.ToString() + "','" + timeToday.ToString() + "','" + dateToday.ToString() + "'," + lbl_DocID.Text + ",'Received');";
                    cmd = new MySqlCommand(receivedDoc, con);
                    int success = cmd.ExecuteNonQuery();


                    if (success > 0)
                    {
                        MessageBox.Show("Document Received!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblStatus.Text = "Received";
                        lblStatus.ForeColor = Color.Green;
                        con.Close();
                    }
                }
                else if(lblStatus.Text == "Received")
                {

                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }
        public void getUnitID()
        {
            con.Open();
            string query = "SELECT unitID FROM unit WHERE name ='"+lblUnitName.Text+"';";

            using (MySqlCommand command = new MySqlCommand(query, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.unitID = reader.GetInt32("unitID");
                    }
                }
            }
            con.Close();
        }

        /*private void DecodeQRCode(Bitmap image)
        {
            try
            {
                // Set up the QR code reader
                BarcodeReader barcodeReader = new BarcodeReader();
                barcodeReader.Options = new DecodingOptions
                {
                    PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
                };

                // Decode the QR code from the provided image
                Result result = barcodeReader.Decode(image);

                if (result != null)
                {
                    // Extract the data from the QR code

                    // Display the decoded data in a message box

                    // Update the label's text on the UI thread
                    if (lblContent.InvokeRequired)
                    {
                        lblContent.Invoke(new Action(() =>
                        {
                            txtData.Text = result.ToString();
                            lblContent.Text = result.ToString();
                        }));
                    }
                    else
                    {
                        txtData.Text = result.ToString();
                        lblContent.Text = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Error occurred while decoding the QR code
            }
        }*/

        private void timer_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = "Pending";
            lblStatus.ForeColor = Color.Orange;
            /*// Decode the QR code from the captured frame
            Result result = barcodeReader.Decode((Bitmap)scanQRFrame.Image);

            if (result != null)
            {
                // Display the decoded information in a message box
                lblContent.Text = "QR Code Information" + result.Text;

                // Stop the timer when a QR code is successfully decoded
                timer.Stop();
            }*/
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Format the date as "yyyy-MM-dd" (e.g., 2023-05-19)
            string Date = today.ToString("yyyy-MM-dd");

            // Get the current time
            DateTime currentTime = DateTime.Now;

            // Format the time as "HH:mm:ss" (e.g., 15:30:45)
            string Time = currentTime.ToString("HH:mm:ss");

            con.Open();
            string addTracking = "INSERT INTO tracking VALUES (''," + unitID + ",'"+cmb_Status.Text+ " by: " + lblUnitName.Text + " Date: " + Date.ToString() + " Time: " + Time.ToString() + " - Remarks: "+txt_Remarks.Text+"','" + Time.ToString() + "','" + Date.ToString() + "'," + lbl_DocID.Text + ",'"+cmb_Status.Text+"');";
            cmd = new MySqlCommand(addTracking, con);
            int success1 = cmd.ExecuteNonQuery();
            

            if (success1 > 0)
            {
                if(cmb_Status.Text == "Released")
                {
                    string updateDoc = "UPDATE documents set status = 'Done' where documentID="+lbl_DocID.Text+"";
                    cmd = new MySqlCommand(updateDoc, con);
                    int success11 = cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Document Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clear();
                con.Close();
            }
        }

        private void lblUnitName_Click(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            txtDateReceived.Text = "";
            txtDocTitle.Text = "";
            txtDocType.Text = "";
            txtForwardedTo.Text = "";
            txtOverdue.Text = "";
            txtTrackingNum.Text = "";
            txt_Agency.Text = "";
            txt_Email.Text = "";
            txt_MobileNo.Text = "";
            txt_Name.Text = "";
            txt_Remarks.Text = "";
            cmb_Status.Text = "";
            lbl_DocID.Text = "";
        }
    }
}
