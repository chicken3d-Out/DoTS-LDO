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
    public partial class DocOwnerAddEdit : Form
    {
        public DocOwnerAddEdit()
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

            try
            {
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
        private void DocOwnerAddEdit_Load(object sender, EventArgs e)
        {
            fillCombo();
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
                    else if (cmb_Unit.Text == "")
                    {
                        MessageBox.Show("Unit Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_Position.Text == "")
                    {
                        MessageBox.Show("Position Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        con.Open();
                        string addUsers = "INSERT INTO docowner VALUES ('','" + txt_name.Text + "','" + lblUnitID.Text + "','" + txt_Position.Text + "');";
                        cmd = new MySqlCommand(addUsers, con);
                        int success = cmd.ExecuteNonQuery();
                        con.Close();

                        if (success > 0)
                        {
                            MessageBox.Show("Document Owner Successfully Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed To Document Owner!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (btnDocOwner.Text == "Update")
            {
                try
                {
                    if (txt_name.Text == "")
                    {
                        MessageBox.Show("Name Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cmb_Unit.Text == "")
                    {
                        MessageBox.Show("Unit Field is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txt_Position.Text == "")
                    {
                        MessageBox.Show("Position is Empty!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        con.Open();
                        string updateUsers = "UPDATE docowner set name='" + txt_name.Text + "', agency= '" + lblUnitID.Text + "', position='" + txt_Position.Text + "' WHERE docOwnerID ='" + lblDocOwnerID.Text + "';";
                        cmd = new MySqlCommand(updateUsers, con);
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
}
