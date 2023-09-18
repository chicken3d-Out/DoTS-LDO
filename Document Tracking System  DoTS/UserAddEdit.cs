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
    public partial class UserAddEdit : Form
    {
        public UserAddEdit()
        {
            InitializeComponent();
        }

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        void fillCombo()
        {
            string query = "SELECT * from unit;";
            MySqlCommand data = new MySqlCommand(query, con);
            MySqlDataReader myReader;

            cmb_Unit.Items.Clear();
            cmb_AccessLvl.Items.Clear();

            try
            {
                //Add Items on AccessLvl
                cmb_AccessLvl.Items.AddRange(new string[] { "Unit", "Admin" });

                con.Open();
                myReader = data.ExecuteReader();
                while (myReader.Read())
                {
                    string unitName = myReader.GetString("name");
                    cmb_Unit.Items.Add(unitName);
                }
                myReader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserAddEdit_Load(object sender, EventArgs e)
        {
            fillCombo();
        }

        private void txtConfirmPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPass.Text != txtConfirmPass.Text)
            {
                lblConfirm.ForeColor = System.Drawing.Color.Red;
                lblConfirm.Text = "Not Matched";
                btnUnit.Enabled = false;
            }
            else
            {
                lblConfirm.ForeColor = System.Drawing.Color.Green;
                lblConfirm.Text = "Matched";
                btnUnit.Enabled = true;
            }
        }

        private void btnUnit_Click(object sender, EventArgs e)
        {
            if( btnUnit.Text == "Add")
            {
                try
                {
                    if (txtFirstname.Text == "")
                    {
                        MessageBox.Show("Firstname Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtLastname.Text == "")
                    {
                        MessageBox.Show("Lastname Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_AccessLvl.Text == "")
                    {
                        MessageBox.Show("Access Level Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_Unit.Text == "")
                    {
                        MessageBox.Show("Unit Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtUsername.Text == "")
                    {
                        MessageBox.Show("Username Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtPass.Text == "")
                    {
                        MessageBox.Show("Password Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtConfirmPass.Text == "")
                    {
                        MessageBox.Show("Confirm Password Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        con.Open();
                        string addUsers = "INSERT INTO users VALUES ('','" + txtFirstname.Text + "','" + txtLastname.Text + "','" + cmb_AccessLvl.Text + "','" + txtUsername.Text + "','"+txtPass.Text+"',"+lblUnitID.Text+");";
                        cmd = new MySqlCommand(addUsers, con);
                        int success = cmd.ExecuteNonQuery();
                        con.Close();

                        if (success > 0)
                        {
                            MessageBox.Show("User Successfully Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed To Create User!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (btnUnit.Text == "Update")
            {
                try
                {
                    if (txtFirstname.Text == "")
                    {
                        MessageBox.Show("Firstname Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtLastname.Text == "")
                    {
                        MessageBox.Show("Lastname Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_AccessLvl.Text == "")
                    {
                        MessageBox.Show("Access Level Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_Unit.Text == "")
                    {
                        MessageBox.Show("Unit Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtUsername.Text == "")
                    {
                        MessageBox.Show("Username Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtPass.Text == "")
                    {
                        MessageBox.Show("Password Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtConfirmPass.Text == "")
                    {
                        MessageBox.Show("Confirm Password Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        con.Open();
                        string updateUsers = "UPDATE users set firstname='" + txtFirstname.Text + "', lastname= '" + txtLastname.Text + "', accesslvl='" + cmb_AccessLvl.Text + "', username ='" + txtUsername.Text + "', password = '" + txtPass.Text + "',unitID ='" + lblUnitID.Text + "' WHERE usersID ='"+lblUserID.Text+"';";
                        cmd = new MySqlCommand(updateUsers, con);
                        int success = cmd.ExecuteNonQuery();
                        con.Close();

                        if (success > 0)
                        {
                            MessageBox.Show("User Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed To Update User!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void cmb_Unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string users = cmb_Unit.GetItemText(cmb_Unit.SelectedItem);

            string quer = "SELECT unitID from unit where Name ='" + users + "';";
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
    }
}
