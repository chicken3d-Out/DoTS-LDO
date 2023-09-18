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
    public partial class AngencyAddEdit : Form
    {
        public AngencyAddEdit()
        {
            InitializeComponent();
        }

        //Instantiate Connection to XAMPP Server
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        private void btnDocOwner_Click(object sender, EventArgs e)
        {
            if (btnDocOwner.Text == "Add")
            {
                try
                {
                    if (txt_name.Text == "")
                    {
                        MessageBox.Show("Name Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_Agency.Text == "")
                    {
                        MessageBox.Show("Agency Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_Address.Text == "")
                    {
                        MessageBox.Show("Address Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_ContactNo.Text == "")
                    {
                        MessageBox.Show("Contact Number Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_Email.Text == "")
                    {
                        MessageBox.Show("Email Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        con.Open();
                        string addDocAgency = "INSERT INTO docowneragency VALUES ('','" + txt_name.Text + "','" + txt_Agency.Text + "','" + txt_Address.Text + "','" + txt_ContactNo.Text + "','" + txt_Email.Text + "');";
                        cmd = new MySqlCommand(addDocAgency, con);
                        int success = cmd.ExecuteNonQuery();
                        con.Close();

                        if (success > 0)
                        {
                            MessageBox.Show("Document Owner Successfully Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed To Create Document Owner!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (btnDocOwner.Text == "Update")
            {
                try
                {
                    if (txt_name.Text == "")
                    {
                        MessageBox.Show("Name Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_Agency.Text == "")
                    {
                        MessageBox.Show("Agency Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_Address.Text == "")
                    {
                        MessageBox.Show("Address Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_ContactNo.Text == "")
                    {
                        MessageBox.Show("Contact Number Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_Email.Text == "")
                    {
                        MessageBox.Show("Email Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        con.Open();
                        string updateDocAgency = "UPDATE docowneragency set name ='" + txt_name.Text + "',agencyName ='" + txt_Agency.Text + "',address ='" + txt_Address.Text + "', contactNo='" + txt_ContactNo.Text + "',email ='" + txt_Email.Text + "' where docagencyID=" + lblDocOwnerID.Text + ";";
                        cmd = new MySqlCommand(updateDocAgency, con);
                        int success = cmd.ExecuteNonQuery();
                        con.Close();

                        if (success > 0)
                        {
                            MessageBox.Show("Document Owner Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed To Update Document Owner!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
