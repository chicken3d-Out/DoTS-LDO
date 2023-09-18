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
    public partial class Users : UserControl
    {
        int indexRow;
        public Users()
        {
            InitializeComponent();

            btnAdmin.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnAllUser.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnUnits.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnAdd.FlatAppearance.MouseOverBackColor = Color.Transparent;

            btnAdmin.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnAllUser.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnUnits.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnAdd.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btnAdmin.FlatAppearance.BorderColor = Color.White;
            btnAllUser.FlatAppearance.BorderColor = Color.White;
            btnRefresh.FlatAppearance.BorderColor = Color.White;
            btnUnits.FlatAppearance.BorderColor = Color.White;
            btnAdd.FlatAppearance.BorderColor = Color.White;

            //Panel initial Position
            /*panelBottom.Width = btnAllUser.Width;*/
            panelBottom.Location = new Point(18, 55);
            lblSection.Text = "All User";
        }

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAllUser_Click(object sender, EventArgs e)
        {
            /*panelBottom.Width = btnAllUser.Width;*/
            panelBottom.Location = new Point(18, 55);
            lblSection.Text = "All User";

            reloadData();
        }

        private void btnUnits_Click(object sender, EventArgs e)
        {
            /*panelBottom.Width = btnUnits.Width;*/
            panelBottom.Location = new Point(179, 55);
            lblSection.Text = "Units";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select users.usersID as 'ID', unit.name as 'Unit Name', users.firstname as 'First Name',users.lastname as 'Last Name', users.accesslvl as 'Access Level',users.username as 'Username' from unit, users where unit.unitID = users.unitID AND users.accesslvl = 'Unit';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUsers.DataSource = dtable;



                //disable editing cell
                dataGridViewUsers.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUsers.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUsers.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search unit name here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            /*panelBottom.Width = btnAdmin.Width;*/
            panelBottom.Location = new Point(342, 55);
            lblSection.Text = "Admin";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select users.usersID as 'ID', unit.name as 'Unit Name', users.firstname as 'First Name',users.lastname as 'Last Name', users.accesslvl as 'Access Level',users.username as 'Username' from unit, users where unit.unitID = users.unitID AND users.accesslvl = 'Admin';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUsers.DataSource = dtable;



                //disable editing cell
                dataGridViewUsers.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUsers.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUsers.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search unit name here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void txt_Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                /*reloadData();*/

                //Get the Index per cell click
                indexRow = e.RowIndex;
                DataGridViewRow row = dataGridViewUsers.Rows[indexRow];

                if (e.RowIndex > -1)
                {
                    dataGridViewUsers.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#006837");
                    dataGridViewUsers.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    dataGridViewUsers.Rows[e.RowIndex].Selected = true;
                }

            }
            catch
            {

            }
        }

        private void dataGridViewUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UserAddEdit updateUser = new UserAddEdit();
            DataGridViewRow getData = dataGridViewUsers.Rows[indexRow];

            //instantiate the value
            updateUser.lblUserID.Text = Convert.ToString(getData.Cells[0].Value);
            updateUser.cmb_Unit.Text = Convert.ToString(getData.Cells[1].Value);
            updateUser.txtFirstname.Text = Convert.ToString(getData.Cells[2].Value);
            updateUser.txtLastname.Text = Convert.ToString(getData.Cells[3].Value);
            updateUser.cmb_AccessLvl.Text = Convert.ToString(getData.Cells[4].Value);
            updateUser.txtUsername.Text = Convert.ToString(getData.Cells[5].Value);

            updateUser.lblUserSection.Text = "Update User";
            updateUser.btnUnit.Text = "Update";
            updateUser.txtFirstname.Focus();

            updateUser.ShowDialog();
        }
        public void reloadData()
        {
            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select users.usersID as 'ID', unit.name as 'Unit Name', users.firstname as 'First Name',users.lastname as 'Last Name', users.accesslvl as 'Access Level',users.username as 'Username' from unit, users where users.unitID = unit.unitID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUsers.DataSource = dtable;



                //disable editing cell
                dataGridViewUsers.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUsers.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUsers.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search unit name here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void dataGridViewUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                // Even row index, set the font to bold
                e.CellStyle.Font = new Font("Poppins", 11, FontStyle.Regular);
            }
            else
            {
                // Odd row index, set the font to regular
                e.CellStyle.Font = new Font("Poppins", 11, FontStyle.Regular);
            }
        }

        private void dataGridViewUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                indexRow = e.RowIndex;
                MenuStripDelete.Show(Cursor.Position);
            }
        }

        private void dataGridViewUsers_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewUsers.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridViewUsers.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridViewUsers_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewUsers.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#006837");
                dataGridViewUsers.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dataGridViewUsers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //disable selection
            dataGridViewUsers.ClearSelection();
        }

        private void dataGridViewUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewUsers.ClearSelection();
        }

        private void dataGridViewUsers_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.Font = new Font("Poppins", 11);
        }

        private void dataGridViewUsers_Enter(object sender, EventArgs e)
        {
            reloadData();
        }

        private void txt_Search_Enter(object sender, EventArgs e)
        {
            if (txt_Search.Text != "")
            {
                txt_Search.Text = "";
                txt_Search.ForeColor = Color.Black;
            }
        }

        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Open Connection
                con.Close();
                con.Open();
                string dataTable = "select users.usersID as 'ID', unit.name as 'Unit Name', users.firstname as 'First Name',users.lastname as 'Last Name', users.accesslvl as 'Access Level',users.username as 'Username' from users, unit WHERE unit.unitID = users.unitID AND unit.name LIKE '%" + txt_Search.Text + "%';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUsers.DataSource = dtable;

                //disable editing cell
                dataGridViewUsers.ReadOnly = true;

                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUsers.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUsers.Visible = true;
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void txt_Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //Open Connection
                con.Close();
                con.Open();
                string dataTable = "select users.usersID as 'ID', unit.name as 'Unit Name', users.firstname as 'First Name',users.lastname as 'Last Name', users.accesslvl as 'Access Level',users.username as 'Username' from users, unit WHERE unit.unitID = users.unitID AND unit.name LIKE '%" + txt_Search.Text + "%';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUsers.DataSource = dtable;

                //disable editing cell
                dataGridViewUsers.ReadOnly = true;

                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUsers.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUsers.Visible = true;
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void txt_Search_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //Open Connection
                con.Close();
                con.Open();
                string dataTable = "select users.usersID as 'ID', unit.name as 'Unit Name', users.firstname as 'First Name',users.lastname as 'Last Name', users.accesslvl as 'Access Level',users.username as 'Username' from users, unit WHERE unit.unitID = users.unitID AND unit.name LIKE '%" + txt_Search.Text + "%';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUsers.DataSource = dtable;

                //disable editing cell
                dataGridViewUsers.ReadOnly = true;

                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUsers.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUsers.Visible = true;
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserAddEdit adduser = new UserAddEdit();
            adduser.lblUserSection.Text = "Add User";
            adduser.btnUnit.Text = "Add";

            adduser.ShowDialog();


        }

        private void Users_Load(object sender, EventArgs e)
        {
            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select users.usersID as 'ID', unit.name as 'Unit Name', users.firstname as 'First Name',users.lastname as 'Last Name', users.accesslvl as 'Access Level',users.username as 'Username' from unit, users where unit.unitID = users.unitID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUsers.DataSource = dtable;



                //disable editing cell
                dataGridViewUsers.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUsers.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUsers.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search unit name here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                //Open Connection

                reloadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void txt_Search_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Search.Text))
            {
                txt_Search.Text = "Search unit name here...";
                txt_Search.ForeColor = Color.Gray;
            }
        }

        private void dataGridViewUsers_Enter_1(object sender, EventArgs e)
        {
            reloadData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow getID = dataGridViewUsers.Rows[indexRow];
                //check if there is a selected row
                if (dataGridViewUsers.SelectedCells.Count <= 0)
                {
                    MessageBox.Show("You did not select a row!", "Select Row!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are You Sure To Permanently Delete This Data?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        string delete = "DELETE FROM users WHERE usersID =" + getID.Cells[0].Value + ";";
                        cmd = new MySqlCommand(delete, con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        //updates the deleted row from the datagridview automatically
                        int rowIndex = dataGridViewUsers.SelectedRows[0].Index;
                        dataGridViewUsers.Rows.RemoveAt(rowIndex);
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
