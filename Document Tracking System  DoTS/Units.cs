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
    public partial class Units : UserControl
    {
        public Units()
        {
            InitializeComponent();

            btnFilter.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnNewUnit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.Transparent;

            btnFilter.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnNewUnit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnRefresh.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btnFilter.FlatAppearance.BorderColor = Color.White;
            btnNewUnit.FlatAppearance.BorderColor = Color.White;
            btnRefresh.FlatAppearance.BorderColor = Color.White;

            //Refresh every second
            /*Timer timer = new Timer();
            timer.Interval = 1000; // Refresh every 10 seconds
            timer.Tick += Timer_Tick;
            timer.Start();*/
        }

        int indexRow;

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();

        private void btnNewUnit_Click(object sender, EventArgs e)
        {
            AddUnit addunit = new AddUnit();
            addunit.ShowDialog();
        }
        private void dataGridViewUnits_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            

            if (e.RowIndex > -1)
            {
                dataGridViewUnits.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#006837");
                dataGridViewUnits.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dataGridViewUnits_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridViewUnits.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridViewUnits.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridViewUnits_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewUnits.ClearSelection();
        }

        private void dataGridViewUnits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                /*reloadData();*/

                //Get the Index per cell click
                indexRow = e.RowIndex;
                DataGridViewRow row = dataGridViewUnits.Rows[indexRow];

                if (e.RowIndex > -1)
                {
                    dataGridViewUnits.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#006837");
                    dataGridViewUnits.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    dataGridViewUnits.Rows[e.RowIndex].Selected = true;
                }

            }
            catch
            {

            }
        }

        /*private void Timer_Tick(object sender, EventArgs e)
        {
            // Refresh the DataGridView control
            reloadData();
        }*/

        public void reloadData()
        {
            try
            {
                //Open Connection

                con.Open();
                string dataTable = "SELECT unitID as 'ID', name as 'Unit Name', email as 'Email',phoneNum as 'Phone Number', located as 'Located' from unit;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUnits.DataSource = dtable;



                //disable editing cell
                dataGridViewUnits.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUnits.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUnits.Visible = true;
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

        public void Units_Load(object sender, EventArgs e)
        {
            try
            {
                //Open Connection

                con.Open();
                string dataTable = "SELECT unitID as 'ID', name as 'Unit Name', email as 'Email',phoneNum as 'Phone Number', located as 'Located' from unit;";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUnits.DataSource = dtable;

                

                //disable editing cell
                dataGridViewUnits.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUnits.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUnits.Visible = true;
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
                reloadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void dataGridViewUnits_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void dataGridViewUnits_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //disable selection
            dataGridViewUnits.ClearSelection();
        }
        private void dataGridViewUnits_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.Font = new Font("Poppins", 11);
        }

        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Open Connection
                con.Close();
                con.Open();
                string dataTable = "SELECT unitID as 'ID', name as 'Unit Name', email as 'Email',phoneNum as 'Phone Number', located as 'Located' from unit WHERE Name LIKE '%" + txt_Search.Text + "%';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUnits.DataSource = dtable;

                //disable editing cell
                dataGridViewUnits.ReadOnly = true;

                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUnits.Visible = false;
                }else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUnits.Visible = true;
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
                string dataTable = "SELECT unitID as 'ID', name as 'Unit Name', email as 'Email',phoneNum as 'Phone Number', located as 'Located' from unit WHERE Name LIKE '%" + txt_Search.Text + "%';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUnits.DataSource = dtable;

                //disable editing cell
                dataGridViewUnits.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUnits.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUnits.Visible = true;
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
                string dataTable = "SELECT unitID as 'ID', name as 'Unit Name', email as 'Email',phoneNum as 'Phone Number', located as 'Located' from unit WHERE Name LIKE '%" + txt_Search.Text + "%';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewUnits.DataSource = dtable;

                //disable editing cell
                dataGridViewUnits.ReadOnly = true;

                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewUnits.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewUnits.Visible = true;
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ContextMenuStrip.Show(Cursor.Position);
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
                txt_Search.Text = "Search unit name here...";
                txt_Search.ForeColor = Color.Gray;
            }
        }

        private void dataGridViewUnits_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                indexRow = e.RowIndex;
                MenuStripDelete.Show(Cursor.Position);
            }
        }

        private void dataGridViewUnits_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateUnits updateUnit = new UpdateUnits();
            DataGridViewRow getData = dataGridViewUnits.Rows[indexRow];

            //instantiate the value
            updateUnit.lblUnitID.Text = Convert.ToString(getData.Cells[0].Value);
            updateUnit.txt_name.Text = Convert.ToString(getData.Cells[1].Value);
            updateUnit.txt_email.Text = Convert.ToString(getData.Cells[2].Value);
            updateUnit.txt_PhoneNo.Text = Convert.ToString(getData.Cells[3].Value);
            updateUnit.txt_Located.Text = Convert.ToString(getData.Cells[4].Value);

            updateUnit.txt_name.Focus();

            updateUnit.ShowDialog();

            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow getID = dataGridViewUnits.Rows[indexRow];
                //check if there is a selected row
                if (dataGridViewUnits.SelectedCells.Count <= 0)
                {
                    MessageBox.Show("You did not select a row!", "Select Row!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are You Sure To Permanently Delete This Data?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        string delete = "DELETE FROM unit WHERE unitID =" + getID.Cells[0].Value + ";";
                        cmd = new MySqlCommand(delete, con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        //updates the deleted row from the datagridview automatically
                        int rowIndex = dataGridViewUnits.SelectedRows[0].Index;
                        dataGridViewUnits.Rows.RemoveAt(rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void dataGridViewUnits_Enter(object sender, EventArgs e)
        {
            reloadData();
        }

        private void dataGridViewUnits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
