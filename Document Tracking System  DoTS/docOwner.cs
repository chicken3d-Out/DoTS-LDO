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
    public partial class DocOwner : UserControl
    {
        public DocOwner()
        {
            InitializeComponent();

            btnAddOwner.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnAgency.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnUnits.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnOthers.FlatAppearance.MouseOverBackColor = Color.Transparent;

            btnAddOwner.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnAgency.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnUnits.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnOthers.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btnAddOwner.FlatAppearance.BorderColor = Color.White;
            btnAgency.FlatAppearance.BorderColor = Color.White;
            btnRefresh.FlatAppearance.BorderColor = Color.White;
            btnUnits.FlatAppearance.BorderColor = Color.White;
            btnOthers.FlatAppearance.BorderColor = Color.White;
        }
        int indexRow;

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        public void searchDocOwner()
        {
            if(lblSection.Text == "Unit")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select docowner.docOwnerID as 'ID',docowner.name as 'Owner', unit.name as 'Unit Name', docowner.position as 'Position',unit.email as 'Email', unit.phoneNum as 'Phone Number' from unit, docowner where docowner.agency = unit.unitID AND docowner.name LIKE '%" + txt_Search.Text + "%';";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocOwner.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocOwner.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocOwner.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocOwner.Visible = true;
                    }

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if(lblSection.Text == "Agency")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select docagencyID as 'ID', name as 'Owner', agencyName as 'Agency Name', address as 'Address', contactNo as 'Contact No', email as 'Email' from docowneragency WHERE docowneragency.name LIKE '%" + txt_Search.Text + "%';";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocOwner.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocOwner.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocOwner.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocOwner.Visible = true;
                    }

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if (lblSection.Text == "Others")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select docothersID as 'ID', name as 'Owner', agencyName as 'Agency Name', address as 'Address', contactNo as 'Contact No', email as 'Email' from docownerothers WHERE docownerothers.name LIKE '%" + txt_Search.Text + "%';";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocOwner.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocOwner.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocOwner.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocOwner.Visible = true;
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
        }
        public void reloadData()
        {
            if(lblSection.Text == "Unit")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select docowner.docOwnerID as 'ID',docowner.name as 'Owner', unit.name as 'Unit Name', docowner.position as 'Position',unit.email as 'Email', unit.phoneNum as 'Phone Number' from unit, docowner where docowner.agency = unit.unitID;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocOwner.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocOwner.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocOwner.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocOwner.Visible = true;
                    }

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if(lblSection.Text == "Agency")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select docagencyID as 'ID', name as 'Owner', agencyName as 'Agency Name', address as 'Address', contactNo as 'Contact No', email as 'Email' from docowneragency;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocOwner.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocOwner.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocOwner.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocOwner.Visible = true;
                    }

                    con.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if(lblSection.Text == "Others")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select docothersID as 'ID', name as 'Owner', agencyName as 'Agency Name', address as 'Address', contactNo as 'Contact No', email as 'Email' from docownerothers;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocOwner.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocOwner.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocOwner.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocOwner.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    txt_Search.Text = "Search document owner here...";
                    txt_Search.ForeColor = Color.Gray;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }
        private void btnAgency_Click(object sender, EventArgs e)
        {
            
            panelBottom.Location = new Point(178, 54);
            lblSection.Text = "Agency";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select docagencyID as 'ID', name as 'Owner', agencyName as 'Agency Name', address as 'Address', contactNo as 'Contact No', email as 'Email' from docowneragency;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewDocOwner.DataSource = dtable;



                //disable editing cell
                dataGridViewDocOwner.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewDocOwner.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewDocOwner.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search document owner here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnOthers_Click(object sender, EventArgs e)
        {
            panelBottom.Location = new Point(322, 54);
            lblSection.Text = "Others";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select docothersID as 'ID', name as 'Owner', agencyName as 'Agency Name', address as 'Address', contactNo as 'Contact No', email as 'Email' from docownerothers;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewDocOwner.DataSource = dtable;



                //disable editing cell
                dataGridViewDocOwner.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewDocOwner.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewDocOwner.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search document owner here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnUnits_Click(object sender, EventArgs e)
        {
            panelBottom.Location = new Point(20, 54);
            lblSection.Text = "Unit";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select docowner.docOwnerID as 'ID',docowner.name as 'Owner', unit.name as 'Unit Name', docowner.position as 'Position',unit.email as 'Email', unit.phoneNum as 'Phone Number' from unit, docowner where docowner.agency = unit.unitID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewDocOwner.DataSource = dtable;



                //disable editing cell
                dataGridViewDocOwner.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewDocOwner.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewDocOwner.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search document owner here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnAddOwner_Click(object sender, EventArgs e)
        {
            MenuStripAdd.Show(Cursor.Position);
        }

        private void unitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocOwnerAddEdit docUnit = new DocOwnerAddEdit();

            docUnit.lblDocOwnerSection.Text = "Add Unit Doc Owner";
            docUnit.btnDocOwner.Text = "Add";
            docUnit.ShowDialog();
        }

        private void agencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AngencyAddEdit agencadd = new AngencyAddEdit();
            agencadd.lblDocOwnerSection.Text = "Add Agency Doc Owner";
            agencadd.btnDocOwner.Text = "Add";
            agencadd.ShowDialog();
        }

        private void DocOwner_Load(object sender, EventArgs e)
        {
            lblSection.Text = "Unit";
            panelBottom.Location = new Point(20, 54);
            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select docowner.docOwnerID as 'ID',docowner.name as 'Owner', unit.name as 'Unit Name', docowner.position as 'Position',unit.email as 'Email', unit.phoneNum as 'Phone Number' from unit, docowner where docowner.agency = unit.unitID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewDocOwner.DataSource = dtable;



                //disable editing cell
                dataGridViewDocOwner.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewDocOwner.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewDocOwner.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search document owner here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void dataGridViewDocOwner_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                /*reloadData();*/

                //Get the Index per cell click
                indexRow = e.RowIndex;
                DataGridViewRow row =  dataGridViewDocOwner.Rows[indexRow];

                if (e.RowIndex > -1)
                {
                    dataGridViewDocOwner.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#006837");
                    dataGridViewDocOwner.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    dataGridViewDocOwner.Rows[e.RowIndex].Selected = true;
                }
            }
            catch
            {

            }
        }

        private void dataGridViewDocOwner_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(lblSection.Text == "Unit")
            {
                DocOwnerAddEdit docOwnerUnit = new DocOwnerAddEdit();
                DataGridViewRow getData = dataGridViewDocOwner.Rows[indexRow];

                //instantiate the value
                docOwnerUnit.lblDocOwnerID.Text = Convert.ToString(getData.Cells[0].Value);
                docOwnerUnit.txt_name.Text = Convert.ToString(getData.Cells[1].Value);
                docOwnerUnit.cmb_Unit.Text = Convert.ToString(getData.Cells[2].Value);
                docOwnerUnit.txt_Position.Text = Convert.ToString(getData.Cells[3].Value);

                docOwnerUnit.lblDocOwnerSection.Text = "Update Unit Document Owner";
                docOwnerUnit.btnDocOwner.Text = "Update";
                docOwnerUnit.txt_name.Focus();

                docOwnerUnit.ShowDialog();
            }
            else if (lblSection.Text == "Agency")
            {
                AngencyAddEdit agencyDocOwner = new AngencyAddEdit();
                DataGridViewRow getData = dataGridViewDocOwner.Rows[indexRow];

                //instantiate the value
                agencyDocOwner.lblDocOwnerID.Text = Convert.ToString(getData.Cells[0].Value);
                agencyDocOwner.txt_name.Text = Convert.ToString(getData.Cells[1].Value);
                agencyDocOwner.txt_Agency.Text = Convert.ToString(getData.Cells[2].Value);
                agencyDocOwner.txt_Address.Text = Convert.ToString(getData.Cells[3].Value);
                agencyDocOwner.txt_ContactNo.Text = Convert.ToString(getData.Cells[4].Value);
                agencyDocOwner.txt_Email.Text = Convert.ToString(getData.Cells[5].Value);

                agencyDocOwner.lblDocOwnerSection.Text = "UpdateAgency Document Owner";
                agencyDocOwner.btnDocOwner.Text = "Update";
                agencyDocOwner.txt_name.Focus();

                agencyDocOwner.ShowDialog();

            }
            else if (lblSection.Text == "Others")
            {
                OthersAddEdit docOwnerOther = new OthersAddEdit();
                DataGridViewRow getData = dataGridViewDocOwner.Rows[indexRow];

                //instantiate the value
                docOwnerOther.lblDocOwnerID.Text = Convert.ToString(getData.Cells[0].Value);
                docOwnerOther.txt_name.Text = Convert.ToString(getData.Cells[1].Value);
                docOwnerOther.txt_Agency.Text = Convert.ToString(getData.Cells[2].Value);
                docOwnerOther.txt_Address.Text = Convert.ToString(getData.Cells[3].Value);
                docOwnerOther.txt_ContactNo.Text = Convert.ToString(getData.Cells[4].Value);
                docOwnerOther.txt_Email.Text = Convert.ToString(getData.Cells[5].Value);

                docOwnerOther.lblDocOwnerSection.Text = "Update Agency Document Owner";
                docOwnerOther.btnDocOwner.Text = "Update";
                docOwnerOther.txt_name.Focus();

                docOwnerOther.ShowDialog();
            }
            
        }

        private void dataGridViewDocOwner_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void dataGridViewDocOwner_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                indexRow = e.RowIndex;
                MenuStripDelete.Show(Cursor.Position);
            }
        }

        private void dataGridViewDocOwner_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewDocOwner.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridViewDocOwner.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridViewDocOwner_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewDocOwner.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#006837");
                dataGridViewDocOwner.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dataGridViewDocOwner_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewDocOwner.ClearSelection();
        }

        private void dataGridViewDocOwner_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewDocOwner.ClearSelection();
        }

        private void othersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OthersAddEdit docOwnerOther = new OthersAddEdit();
            docOwnerOther.lblDocOwnerSection.Text = "Add Others Doc Owner";
            docOwnerOther.btnDocOwner.Text = "Add";
            docOwnerOther.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            reloadData();
        }

        private void dataGridViewDocOwner_Click(object sender, EventArgs e)
        {
            /*reloadData();*/
        }

        private void txt_Search_Enter(object sender, EventArgs e)
        {
            if (txt_Search.Text != "")
            {
                txt_Search.Text = "";
                txt_Search.ForeColor = Color.Black;
            }
        }

        private void txt_Search_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Search.Text))
            {
                txt_Search.Text = "Search document owner here...";
                txt_Search.ForeColor = Color.Gray;
            }
        }

        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            searchDocOwner();
        }

        private void txt_Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            searchDocOwner();
        }

        private void txt_Search_KeyUp(object sender, KeyEventArgs e)
        {
            searchDocOwner();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lblSection.Text == "Unit")
            {
                try
                {
                    DataGridViewRow getID = dataGridViewDocOwner.Rows[indexRow];
                    //check if there is a selected row
                    if (dataGridViewDocOwner.SelectedCells.Count <= 0)
                    {
                        MessageBox.Show("You did not select a row!", "Select Row!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (MessageBox.Show("Are You Sure To Permanently Delete This Data?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            string delete = "DELETE FROM docowner WHERE docownerID =" + getID.Cells[0].Value + ";";
                            cmd = new MySqlCommand(delete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            //updates the deleted row from the datagridview automatically
                            int rowIndex = dataGridViewDocOwner.SelectedRows[0].Index;
                            dataGridViewDocOwner.Rows.RemoveAt(rowIndex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if(lblSection.Text == "Agency")
            {
                try
                {
                    DataGridViewRow getID = dataGridViewDocOwner.Rows[indexRow];
                    //check if there is a selected row
                    if (dataGridViewDocOwner.SelectedCells.Count <= 0)
                    {
                        MessageBox.Show("You did not select a row!", "Select Row!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (MessageBox.Show("Are You Sure To Permanently Delete This Data?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            string delete = "DELETE FROM docownerAgency WHERE docagencyID =" + getID.Cells[0].Value + ";";
                            cmd = new MySqlCommand(delete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            //updates the deleted row from the datagridview automatically
                            int rowIndex = dataGridViewDocOwner.SelectedRows[0].Index;
                            dataGridViewDocOwner.Rows.RemoveAt(rowIndex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if (lblSection.Text == "Others")
            {
                try
                {
                    DataGridViewRow getID = dataGridViewDocOwner.Rows[indexRow];
                    //check if there is a selected row
                    if (dataGridViewDocOwner.SelectedCells.Count <= 0)
                    {
                        MessageBox.Show("You did not select a row!", "Select Row!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (MessageBox.Show("Are You Sure To Permanently Delete This Data?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            string delete = "DELETE FROM docownerOthers WHERE docothersID =" + getID.Cells[0].Value + ";";
                            cmd = new MySqlCommand(delete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            //updates the deleted row from the datagridview automatically
                            int rowIndex = dataGridViewDocOwner.SelectedRows[0].Index;
                            dataGridViewDocOwner.Rows.RemoveAt(rowIndex);
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
