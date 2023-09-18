using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Document_Tracking_System__DoTS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            btnLogin.Enabled = false;
        }


        //Instantiate Connection to XAMPP Server
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        string userType;
        int userID;
        string unitName;


        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {

            // Before calling Application.Exit(), close or hide all open forms
            Environment.Exit(1);
        }

        private void Login_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Username.ToString()))
            {
                btnLogin.Enabled = false;
            }
            else
            {
                btnLogin.Enabled = true;
            }
        }

        private void txt_Username_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Username.ToString()))
            {
                btnLogin.Enabled = false;
            }
            else
            {
                btnLogin.Enabled = true;
            }
        }

        private void checkBox1_TextChanged(object sender, EventArgs e)
        {
   
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_Password.PasswordChar = '\0';
            }
            else
            {
                txt_Password.PasswordChar = '•';
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Username.Text == "")
                {
                    MessageBox.Show("Username Field is Empty", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txt_Password.Text == "")
                {
                    MessageBox.Show("Password Field is Empty", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    /* timer.Start();
                     timer.Interval = 35;
                     timer.Tick += timer_Tick;*/
                    percent.Visible = true;

                    txt_Username.Enabled = false;
                    txt_Password.Enabled = false;
                    btnLogin.Enabled = false;
                   

                    login();

                    /*AuthenticateUser();

                    GetUnitName();

                    if (userType == "Admin")
                    {
                        Dashboard dashboard = new Dashboard();

                        dashboard.btnUnits.Visible = false;
                        dashboard.btnUsers.Visible = false;

                        dashboard.lblUnitName.Text = unitName.ToString();
                        dashboard.lblUserID.Text = userID.ToString();

                        dashboard.lblUserType.Text = userType.ToString();


                        dashboard.Show();
                        this.Hide();
                    }
                    else if (userType == "Unit")
                    {
                        Dashboard dashboard = new Dashboard();

                        dashboard.btnUnits.Visible = false;
                        dashboard.btnUsers.Visible = false;
                        dashboard.btnDocuments.Visible = false;
                        dashboard.btnDocOwner.Visible = false;

                        dashboard.lblUnitName.Text = unitName.ToString();
                        dashboard.lblUserID.Text = userID.ToString();


                        dashboard.Show();
                        this.Hide();
                    }
                    else if (userType == "SuperAdmin")
                    {
                        Dashboard dashboard = new Dashboard();
                        dashboard.lblUnitName.Text = unitName.ToString();
                        dashboard.lblUserID.Text = userID.ToString();

                        dashboard.Show();
                        this.Hide();
                    }*/
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void login()
        {
            try
            {
                AuthenticateUser();

                GetUnitName();

                if (userType == "Admin")
                {
                    Dashboard dashboard = new Dashboard();

                    dashboard.btnUnits.Visible = false;
                    dashboard.btnUsers.Visible = false;

                    dashboard.lblUnitName.Text = unitName.ToString();
                    dashboard.lblUserID.Text = userID.ToString();

                    dashboard.lblUserType.Text = userType.ToString();


                    dashboard.Show();
                    this.Hide();
                }
                else if (userType == "Unit")
                {
                    Dashboard dashboard = new Dashboard();

                    dashboard.btnUnits.Visible = false;
                    dashboard.btnUsers.Visible = false;
                    dashboard.btnDocuments.Visible = false;
                    dashboard.btnDocOwner.Visible = false;

                    dashboard.lblUnitName.Text = unitName.ToString();
                    dashboard.lblUserID.Text = userID.ToString();


                    dashboard.Show();
                    this.Hide();
                }
                else if (userType == "SuperAdmin")
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.lblUnitName.Text = unitName.ToString();
                    dashboard.lblUserID.Text = userID.ToString();

                    dashboard.lblUserType.Text = userType.ToString();

                    dashboard.Show();
                    this.Hide();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void AuthenticateUser()
        {
            con.Open();
            string query = "SELECT usersID,accesslvl FROM users WHERE username ='"+txt_Username.Text+"' AND password = '"+txt_Password.Text+"';";

            using (MySqlCommand command = new MySqlCommand(query, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userType = reader.GetString("accesslvl");
                        userID = reader.GetInt32("usersID");

                        txt_Username.Enabled = false;
                        txt_Password.Enabled = false;
                        btnLogin.Enabled = false;
                        percent.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("Username or Password is Incorrect!");
                        txt_Username.Enabled = true;
                        txt_Password.Enabled = true;
                        btnLogin.Enabled = true;
                        percent.Visible = false;
                        txt_Username.Clear();
                        txt_Password.Clear();
                    }
                }
            }
            con.Close();
        }

        public void GetUnitName()
        {
            con.Open();

            string query = "SELECT unit.name as 'unitName' FROM users, unit where users.usersID ="+userID+" AND users.unitID = unit.unitID;";

            using (MySqlCommand command = new MySqlCommand(query, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        unitName = reader.GetString("unitName");
                    }
                }
            }
            con.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            /*if (progressBar.Value < 100)
            {
                percent.Visible = true;
                txt_Username.Enabled = false;
                txt_Password.Enabled = false;
                btnLogin.Enabled = false;
                progressBar.Value++;
                percent.Text = "" + progressBar.Value + " %";
            }
            else
            {
                timer.Enabled = false;

            }*/
            
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        /*private void iconPictureBox1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to close?", "Close Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }*/
    }
}
