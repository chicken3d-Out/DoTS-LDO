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
    public partial class DocumentInfo : UserControl
    {
        public DocumentInfo()
        {
            InitializeComponent();
            btnNewDocument.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnUnits.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnAgency.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnOthers.FlatAppearance.MouseOverBackColor = Color.Transparent;

            btnNewDocument.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnUnits.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnAgency.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnOthers.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btnNewDocument.FlatAppearance.BorderColor = Color.White;
            btnRefresh.FlatAppearance.BorderColor = Color.White;
            btnUnits.FlatAppearance.BorderColor = Color.White;
            btnAgency.FlatAppearance.BorderColor = Color.White;
            btnOthers.FlatAppearance.BorderColor = Color.White;


        }

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        int indexRow;


        private void btnNewDocument_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void btnUnits_Click(object sender, EventArgs e)
        {
            dataGridViewDocuments.CellFormatting += dataGridViewDocuments_CellFormatting;

            pnl_BookMark.Location = new Point(32, 48);
            lbl_Section.Text = "/ Unit";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select documents.trackingNo as 'Tracking No', docowner.name as 'Name', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, unit, docowner where documents.docOwnerID = docowner.docOwnerID AND documents.forwardedTo = unit.unitID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewDocuments.DataSource = dtable;



                //disable editing cell
                dataGridViewDocuments.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewDocuments.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewDocuments.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_search.Text = "Search document owner here...";
                txt_search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnAgency_Click(object sender, EventArgs e)
        {
            dataGridViewDocuments.CellFormatting += dataGridViewDocuments_CellFormatting;

            pnl_BookMark.Location = new Point(188, 48);
            lbl_Section.Text = "/ Agency";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select documents.trackingNo as 'Tracking No', docowneragency.name as 'Name',docowneragency.agencyName as 'From', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, docowneragency,unit where documents.docAgencyID = docowneragency.docAgencyID AND documents.forwardedTo = unit.unitID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewDocuments.DataSource = dtable;



                //disable editing cell
                dataGridViewDocuments.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewDocuments.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewDocuments.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_search.Text = "Search document owner here...";
                txt_search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        public void reloadData()
        {
            if (lbl_Section.Text == "/ Unit")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', docowner.name as 'Name',unit.name as 'From', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, unit, docowner where documents.docOwnerID = docowner.docOwnerID AND documents.forwardedTo = unit.unitID AND docowner.agency = unit.unitID;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocuments.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocuments.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocuments.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocuments.Visible = true;
                    }

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

                //Display placeholder
                txt_search.Text = "Search document owner here...";
                txt_search.ForeColor = Color.Gray;

            }
            else if (lbl_Section.Text == "/ Agency")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', docowneragency.name as 'Name',docowneragency.agencyName as 'From', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, docowneragency,unit where documents.docAgencyID = docowneragency.docAgencyID AND documents.forwardedTo = unit.unitID;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocuments.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocuments.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocuments.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocuments.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    txt_search.Text = "Search document owner here...";
                    txt_search.ForeColor = Color.Gray;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if (lbl_Section.Text == "/ Others")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', docownerothers.name as 'Name',docownerothers.agencyName as 'From', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, docownerothers,unit where documents.docOthersID = docownerothers.docOthersID AND documents.forwardedTo = unit.unitID;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocuments.DataSource = dtable;

                    //disable editing cell
                    dataGridViewDocuments.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocuments.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocuments.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    txt_search.Text = "Search document owner here...";
                    txt_search.ForeColor = Color.Gray;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private void btnOthers_Click(object sender, EventArgs e)
        {
            dataGridViewDocuments.CellFormatting += dataGridViewDocuments_CellFormatting;

            pnl_BookMark.Location = new Point(339, 48);
            lbl_Section.Text = "/ Others";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select documents.trackingNo as 'Tracking No', docownerothers.name as 'Name',docownerothers.agencyName as 'From', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, docownerothers,unit where documents.docOthersID = docownerothers.docOthersID AND documents.forwardedTo = unit.unitID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewDocuments.DataSource = dtable;

                //disable editing cell
                dataGridViewDocuments.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewDocuments.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewDocuments.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_search.Text = "Search document owner here...";
                txt_search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void DocumentInfo_Load(object sender, EventArgs e)
        {
            dataGridViewDocuments.CellFormatting += dataGridViewDocuments_CellFormatting;

            pnl_BookMark.Location = new Point(32, 48);
            lbl_Section.Text = "/ Unit";

            try
            {
                //Open Connection

                con.Open();
                string dataTable = "select documents.trackingNo as 'Tracking No',docowner.name as 'Name', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, unit, docowner where documents.docOwnerID = docowner.docOwnerID AND documents.forwardedTo = unit.unitID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewDocuments.DataSource = dtable;



                //disable editing cell
                dataGridViewDocuments.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewDocuments.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewDocuments.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_search.Text = "Search document owner here...";
                txt_search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            reloadData();
        }

        private void dataGridViewDocuments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                /*reloadData();*/

                //Get the Index per cell click
                indexRow = e.RowIndex;
                DataGridViewRow row = dataGridViewDocuments.Rows[indexRow];

                if (e.RowIndex > -1)
                {
                    dataGridViewDocuments.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#006837");
                    dataGridViewDocuments.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    dataGridViewDocuments.Rows[e.RowIndex].Selected = true;
                }
            }
            catch
            {

            }
        }

        private void dataGridViewDocuments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

            int DateColumnIndex = dataGridViewDocuments.Columns["Overdue Date"].Index;
            // Check if the column index is the one you want to apply the formatting to
            if (e.ColumnIndex == DateColumnIndex)
            {
                // Assuming the date value is stored in a DateTime column named "DateColumn"
                if (e.Value != null && e.Value is DateTime dateValue)
                {
                    /*int overdue = DateTime.Now.Subtract(dateValue).Days;*/
                    DateTime today = DateTime.Today;
                    int result = DateTime.Compare(today, dateValue);

                    if (result > 0)
                    {
                        // Set the cell's back color to the desired color
                        e.CellStyle.BackColor = Color.OrangeRed;
                        e.CellStyle.ForeColor = Color.White;
                        // dateToday is greater than date2
                        // Perform actions or logic for this case
                    }
                }
            }
        }

        private void dataGridViewDocuments_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewDocuments.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridViewDocuments.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridViewDocuments_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewDocuments.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#006837");
                dataGridViewDocuments.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dataGridViewDocuments_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewDocuments.ClearSelection();
        }

        private void dataGridViewDocuments_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewDocuments.ClearSelection();
        }

        private void txt_search_Enter(object sender, EventArgs e)
        {
            if (txt_search.Text != "")
            {
                txt_search.Text = "";
                txt_search.ForeColor = Color.Black;
            }
        }

        private void txt_search_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_search.Text))
            {
                txt_search.Text = "Search document owner here...";
                txt_search.ForeColor = Color.Gray;
            }
        }

        public void searchDocOwner()
        {
            if (lbl_Section.Text == "/ Unit")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No',docowner.name as 'Name',unit.name as 'From', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, unit, docowner where documents.docOwnerID = docowner.docOwnerID AND documents.forwardedTo = unit.unitID AND docowner.agency = unit.unitID AND docowner.name LIKE '%" + txt_search.Text+"%';";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocuments.DataSource = dtable;



                    //disable editing cell
                    dataGridViewDocuments.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocuments.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocuments.Visible = true;
                    }

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if (lbl_Section.Text == "/ Agency")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', docowneragency.name as 'Name',docowneragency.agencyName as 'From', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, docowneragency,unit where documents.docAgencyID = docowneragency.docAgencyID AND documents.forwardedTo = unit.unitID AND docowneragency.name LIKE '%" + txt_search.Text+"%';";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocuments.DataSource = dtable;

                    //disable editing cell
                    dataGridViewDocuments.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocuments.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocuments.Visible = true;
                    }

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }

            }
            else if (lbl_Section.Text == "/ Others")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', docownerothers.name as 'Name',docownerothers.agencyName as 'From', documents.docTitle as 'Document Title', documents.documentType as 'Document Type', unit.name as 'Forwarded To', documents.status as 'Status', documents.dateExpired as 'Overdue Date' from documents, docownerothers,unit where documents.docOthersID = docownerothers.docOthersID AND documents.forwardedTo = unit.unitID AND docownerothers.name LIKE '%" + txt_search.Text+"%';";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewDocuments.DataSource = dtable;

                    //disable editing cell
                    dataGridViewDocuments.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewDocuments.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewDocuments.Visible = true;
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

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            searchDocOwner();
        }

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            searchDocOwner();
        }

        private void txt_search_KeyUp(object sender, KeyEventArgs e)
        {
            searchDocOwner();
        }

        private void dataGridViewDocuments_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                indexRow = e.RowIndex;
                MenuStripDelete.Show(Cursor.Position);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow getID = dataGridViewDocuments.Rows[indexRow];
                //check if there is a selected row
                if (dataGridViewDocuments.SelectedCells.Count <= 0)
                {
                    MessageBox.Show("You did not select a row!", "Select Row!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are You Sure To Permanently Delete This Data?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        string delete = "DELETE FROM documents WHERE documentID =" + getID.Cells[0].Value + ";";
                        cmd = new MySqlCommand(delete, con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        //updates the deleted row from the datagridview automatically
                        int rowIndex = dataGridViewDocuments.SelectedRows[0].Index;
                        dataGridViewDocuments.Rows.RemoveAt(rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }

        }

        private void dataGridViewDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
