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
    public partial class UpdateUnits : Form
    {
        public UpdateUnits()
        {
            InitializeComponent();
        }

        //Instantiate Connection to XAMPP Server
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_name.Text == "")
                {
                    MessageBox.Show("Name Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txt_email .Text == "")
                {
                    MessageBox.Show("Email Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txt_PhoneNo.Text == "")
                {
                    MessageBox.Show("Phone Number Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txt_Located.Text == "")
                {
                    MessageBox.Show("Location Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    con.Open();
                    string updateUnit = "UPDATE unit SET name = '" + txt_name.Text + "', email = '" + txt_email.Text + "',phoneNum ='" + txt_PhoneNo.Text + "', located = '" + txt_Located.Text + "' WHERE unitID = " + lblUnitID.Text + ";";
                    cmd = new MySqlCommand(updateUnit, con);
                    int success = cmd.ExecuteNonQuery();
                    con.Close();

                    if (success > 0)
                    {
                        MessageBox.Show("Successfully Update Unit!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed To Update Unit!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }
    }
}
