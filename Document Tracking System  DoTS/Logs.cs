using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Document_Tracking_System__DoTS
{
    public partial class Logs : UserControl
    {
        public Logs()
        {
            InitializeComponent();

            btnRefresh.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.BorderColor = Color.White;

            btnDownload.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnDownload.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnDownload.FlatAppearance.BorderColor = Color.White;
        }

        int indexRow;

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();
       
        private void Logs_Load(object sender, EventArgs e)
        {
            lbl_NoData.Visible = true;
            dataGridViewLogs.Visible = false;

            txt_Search.Text = "Search Tracking Number Here...";
            txt_Search.ForeColor = Color.Gray;
            /*try
            {
                //Open Connection

                con.Open();
                string dataTable = "select documents.docTitle as 'Doc Title',documents.documentType as 'Doc Type', tracking.description as 'Description', documents.status 'Status' from tracking, documents, unit WHERE tracking.location = "+lbl_UnitID.Text+" AND tracking.location = unit.unitID AND documents.documentID = tracking.documentID;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewLogs.DataSource = dtable;

                //disable editing cell
                dataGridViewLogs.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewLogs.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewLogs.Visible = true;
                }

                con.Close();

                //Display placeholder
                txt_Search.Text = "Search Tracking Number Here...";
                txt_Search.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }*/
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialCard3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(lblUserType.Text == "SuperAdmin")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', documents.docTitle as 'Doc Title',documents.documentType as 'Doc Type', tracking.description as 'Description', documents.status 'Status' from tracking, documents, unit WHERE tracking.location = unit.unitID AND documents.documentID = tracking.documentID;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewLogs.DataSource = dtable;



                    //disable editing cell
                    dataGridViewLogs.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewLogs.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewLogs.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    txt_Search.Text = "Search Tracking Number Here...";
                    txt_Search.ForeColor = Color.Gray;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
                //
                
            }
            else
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', documents.docTitle as 'Doc Title',documents.documentType as 'Doc Type', tracking.description as 'Description', documents.status 'Status' from tracking, documents, unit WHERE tracking.location = " + lbl_UnitID.Text + " AND tracking.location = unit.unitID AND documents.documentID = tracking.documentID;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewLogs.DataSource = dtable;

                    //disable editing cell
                    dataGridViewLogs.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewLogs.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewLogs.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    txt_Search.Text = "Search Tracking Number Here...";
                    txt_Search.ForeColor = Color.Gray;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
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
                txt_Search.Text = "Search Tracking Number Here...";
                txt_Search.ForeColor = Color.Gray;
            }
        }

        private void searchLogs()
        {
            if(lblUserType.Text == "SuperAdmin")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', tracking.description as 'Description', documents.docTitle as 'Doc Title',documents.documentType as 'Doc Type', documents.status 'Status' from tracking, documents, unit WHERE AND tracking.location = unit.unitID AND documents.documentID = tracking.documentID AND documents.trackingNo LIKE '%" + txt_Search.Text + "%';";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewLogs.DataSource = dtable;



                    //disable editing cell
                    dataGridViewLogs.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewLogs.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewLogs.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    /*txt_Search.Text = "Search Tracking Number Here...";
                    txt_Search.ForeColor = Color.Gray;*/

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }else
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', tracking.description as 'Description', documents.docTitle as 'Doc Title',documents.documentType as 'Doc Type', documents.status 'Status' from tracking, documents, unit WHERE tracking.location =" + lbl_UnitID.Text + " AND tracking.location = unit.unitID AND documents.documentID = tracking.documentID AND documents.trackingNo LIKE '%" + txt_Search.Text + "%';";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewLogs.DataSource = dtable;



                    //disable editing cell
                    dataGridViewLogs.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewLogs.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewLogs.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    /*txt_Search.Text = "Search Tracking Number Here...";
                    txt_Search.ForeColor = Color.Gray;*/

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            searchLogs();
        }

        private void txt_Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            searchLogs();
        }

        private void txt_Search_KeyUp(object sender, KeyEventArgs e)
        {
            searchLogs();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void lbl_UnitID_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewLogs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                //Get the Index per cell click
                indexRow = e.RowIndex;
                DataGridViewRow row = dataGridViewLogs.Rows[indexRow];

                if (e.RowIndex > -1)
                {
                    dataGridViewLogs.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#006837");
                    dataGridViewLogs.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    dataGridViewLogs.Rows[e.RowIndex].Selected = true;
                }
            }
            catch
            {

            }
        }

        private void dataGridViewLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void dataGridViewLogs_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewLogs.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridViewLogs.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridViewLogs_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewLogs.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#006837");
                dataGridViewLogs.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dataGridViewLogs_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewLogs.ClearSelection();
        }

        private void dataGridViewLogs_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewLogs.ClearSelection();
        }

        private void dateFilter_Click(object sender, EventArgs e)
        {
            string FromDate = dateFrom.Value.ToString("yyyy-MM-dd");
            string ToDate = dateTo.Value.ToString("yyyy-MM-dd");

            /*MessageBox.Show("From "+fromDate+" : "+"To"+toDate);*/
            if(lblUserType.Text == "SuperAdmin")
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', documents.docTitle as 'Doc Title',documents.documentType as 'Doc Type', tracking.description as 'Description', documents.status 'Status' from tracking, documents, unit WHERE tracking.date between '" + FromDate + "' AND '" + ToDate + "' AND tracking.location = unit.unitID AND documents.documentID = tracking.documentID;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewLogs.DataSource = dtable;

                    //disable editing cell
                    dataGridViewLogs.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewLogs.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewLogs.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    txt_Search.Text = "Search Tracking Number Here...";
                    txt_Search.ForeColor = Color.Gray;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }else
            {
                try
                {
                    //Open Connection

                    con.Open();
                    string dataTable = "select documents.trackingNo as 'Tracking No', documents.docTitle as 'Doc Title',documents.documentType as 'Doc Type', tracking.description as 'Description', documents.status 'Status' from tracking, documents, unit WHERE tracking.location =" + lbl_UnitID.Text + " AND tracking.date between '" + FromDate + "' AND '" + ToDate + "' AND tracking.location = unit.unitID AND documents.documentID = tracking.documentID;";
                    adp = new MySqlDataAdapter(dataTable, con);
                    DataTable dtable = new DataTable();
                    adp.Fill(dtable);

                    //fills the datagridview
                    dataGridViewLogs.DataSource = dtable;

                    //disable editing cell
                    dataGridViewLogs.ReadOnly = true;


                    if (dtable.Rows.Count == 0)
                    {
                        lbl_NoData.Visible = true;
                        dataGridViewLogs.Visible = false;
                    }
                    else
                    {
                        lbl_NoData.Visible = false;
                        dataGridViewLogs.Visible = true;
                    }

                    con.Close();

                    //Display placeholder
                    txt_Search.Text = "Search Tracking Number Here...";
                    txt_Search.ForeColor = Color.Gray;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewLogs.Rows.Count <= 0)
                {
                    MessageBox.Show("There is no data to be exported!", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (MessageBox.Show("Are you sure you want to Export data now to Excel?", "Export To Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //saving datagrid inputs to microsoft excel
                    saveFileDialog.InitialDirectory = @"C:\Downloads\Exports";
                    saveFileDialog.Title = "Save as Excel File";
                    saveFileDialog.FileName = "DoTS Activity Logs Record Date "+DateTime.Today.ToString("yyyy-MM-dd");
                    saveFileDialog.Filter = "Excel Files|*.xlsx";

                    if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        ExcelApp.Application.Workbooks.Add(Type.Missing);

                        //Change properties of the workbook
                        //Change Column Width
                        ExcelApp.Columns.ColumnWidth = 25;
                        //Change Font Size
                        ExcelApp.Rows.Style.Font.Size = 12;

                        //Alignment Center
                        ExcelApp.Rows.HorizontalAlignment = -4108;
                        ExcelApp.Rows.VerticalAlignment = -4108;

                        ExcelApp.Rows.Worksheet.PageSetup.PrintGridlines = true;

                        //Storing header Part in Excel
                        for (int i = 1; i < dataGridViewLogs.Columns.Count + 1; i++)
                        {
                            ExcelApp.Cells[1, i] = dataGridViewLogs.Columns[i - 1].HeaderText;
                        }

                        //Storing each row and column value to excel sheet
                        for (int i = 0; i < dataGridViewLogs.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridViewLogs.Columns.Count; j++)
                            {
                                ExcelApp.Cells[i + 2, j + 1] = dataGridViewLogs.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                        ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName.ToString());
                        ExcelApp.ActiveWorkbook.Saved = true;
                        ExcelApp.Quit();

                        MessageBox.Show("Data Has Been Successfully Exported to Excel File", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }
    }
}
