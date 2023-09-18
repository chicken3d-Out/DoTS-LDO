using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using MySql.Data.MySqlClient;

namespace Document_Tracking_System__DoTS
{
    public partial class Dashboard : Form
    {
        
        public Dashboard()
        {
            
            /*section1.BringToFront();*/

            InitializeComponent();

            //Material Skin
            var skin = MaterialSkinManager.Instance;

            //skin.AddFormToManage(this);
            skin.Theme = MaterialSkinManager.Themes.LIGHT;
            skin.ColorScheme = new ColorScheme(
                    Primary.Green900,
                    Primary.Green800,
                    Primary.Green500,
                    Accent.Red700,
                    TextShade.WHITE
                );

            sidePanel.Top = btnDashboard.Top;
            sidePanel.Height = btnDashboard.Height;

            
            dashboardChartsInfo1.BringToFront();
        }

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        int unit_ID;

        private void Dashboard_Load(object sender, EventArgs e)
        {
            con.Open();
            string query = "SELECT unitID FROM unit WHERE name ='" + lblUnitName.Text + "';";

            using (MySqlCommand command = new MySqlCommand(query, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.unit_ID = reader.GetInt32("unitID");
                    }
                }
            }
            con.Close();

            timer1.Start();
            timer2.Start();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongDateString().ToUpper();
            lblTime.Text = DateTime.Now.ToLongTimeString().ToUpper();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close?", "Close Application", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }else
            {
                this.Hide();

                Login login = new Login();
                login.Show();
            }
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialCard1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            sidePanel.Top = btnDashboard.Top;
            sidePanel.Height = btnDashboard.Height;


            dashboardChartsInfo1.lblUnitName.Text = lblUnitName.Text;
            dashboardChartsInfo1.BringToFront();
        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            sidePanel.Top = btnDocuments.Top;
            sidePanel.Height = btnDocuments.Height;
            documents1.BringToFront();
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            sidePanel.Top = btnLogs.Top;
            sidePanel.Height = btnLogs.Height;

            logs1.lblUnit.Text = lblUnitName.Text;
            logs1.lbl_UnitID.Text = unit_ID.ToString();
            logs1.lblUserType.Text = lblUserType.Text;
            logs1.BringToFront();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            sidePanel.Top = btnUsers.Top;
            sidePanel.Height = btnUsers.Height;
            users1.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void users1_Load(object sender, EventArgs e)
        {

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            sidePanel.Top = btnAbout.Top;
            sidePanel.Height = btnAbout.Height;
            about1.BringToFront();
        }

        private void btnDocOwner_Click(object sender, EventArgs e)
        {
            sidePanel.Top = btnDocOwner.Top;
            sidePanel.Height = btnDocOwner.Height;
            docOwner1.BringToFront();
            /*DocOwner.BringToFront();*/
        }

        private void addDocument1_Load(object sender, EventArgs e)
        {

        }

        private void btnUnits_Click(object sender, EventArgs e)
        {
            sidePanel.Top = btnUnits.Top;
            sidePanel.Height = btnUnits.Height;
            units1.BringToFront();
        }

        private void units1_Load(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dashboardChartsInfo1.lblUnitName.Text = lblUnitName.Text;
            dashboardChartsInfo1.lblUsertype.Text = lblUserType.Text;
        }

        private void btnTracking_Click(object sender, EventArgs e)
        {
            sidePanel.Top = btnTracking.Top;
            sidePanel.Height = btnTracking.Height;

            tracking1.BringToFront();
        }

        private void panel27_Paint(object sender, PaintEventArgs e)
        {

        }

        private void userClick_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MenuStripUser.Show(Cursor.Position);
            }
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login login = new Login();
            login.Show();
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                // Handle actions when the form is minimized
                // For example, you can hide the form or show a system tray icon
            }
        }
    }
}
