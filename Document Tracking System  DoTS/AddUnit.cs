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
    public partial class AddUnit : Form
    {
        public AddUnit()
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
                else if (txt_email.Text == "")
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
                    string addUnit = "INSERT INTO unit VALUES ('','" + txt_name.Text + "','" + txt_email.Text + "','" + txt_PhoneNo.Text + "','" + txt_Located.Text + "');";
                    cmd = new MySqlCommand(addUnit, con);
                    int success = cmd.ExecuteNonQuery();
                    con.Close();

                    if (success > 0)
                    {
                        MessageBox.Show("Unit Successfully Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed To Create Unit!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
