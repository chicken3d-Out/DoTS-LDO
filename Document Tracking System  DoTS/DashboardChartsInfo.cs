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
    public partial class DashboardChartsInfo : UserControl
    {
        public DashboardChartsInfo()
        {
            InitializeComponent();

            btnRefresh.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnScanQR.FlatAppearance.MouseOverBackColor = Color.Transparent;

            btnRefresh.FlatAppearance.BorderColor = Color.White;
            btnScanQR.FlatAppearance.BorderColor = Color.White;

            btnRefresh.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnScanQR.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();


        private void btnAddDocument_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void btnScanQR_Click(object sender, EventArgs e)
        {
            // Check if the form is already open
            if (Application.OpenForms.OfType<ScanQR>().Any())
            {
                // Form is already open, do not show it again
                return;
            }

            // Form is not open, create a new instance and show it as a modal dialog
            ScanQR scanqr = new ScanQR();

            scanqr.lblUnitName.Text = lblUnitName.Text;
            scanqr.lblUsertype.Text = lblUsertype.Text;
            scanqr.Show();
        }

        private void DashboardChartsInfo_Load(object sender, EventArgs e)
        {
            try
            {
                
                //Open Connection

                fillDash();

                con.Open();
                string dataTable = "select documents.docTitle as 'Document Title',unit.name as 'Unit', tracking.action as 'Action', tracking.time as 'Time' from documents, tracking, unit where tracking.documentID = documents.documentID and tracking.location = unit.unitID and DATE(date) = CURDATE();";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewRecent.DataSource = dtable;

                //disable editing cell
                dataGridViewRecent.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewRecent.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewRecent.Visible = true;
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void dataGridViewRecent_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewRecent.ClearSelection();
        }
        private void fillCharts()
        {
            try
            {
                chart1.Series["Overview"].Points.AddXY("Released", lblRelease.Text);
                chart1.Series["Overview"].Points.AddXY("Forwarded", lblForwarded.Text);
                chart1.Series["Overview"].Points.AddXY("Accepted", lblAccepted.Text);
                chart1.Series["Overview"].Points.AddXY("Returned", lblReturned.Text);
                chart1.Series["Overview"].Points.AddXY("Completed", lblCompleted.Text);
                chart1.Series["Overview"].Points.AddXY("Overdue", lblOverdue.Text);
            }
            catch(Exception ex)
            {

            }
        }

        private void fillDash()
        {
            con.Open();

            string query = "SELECT IFNULL(COUNT(*),0) as 'released' FROM tracking WHERE action = 'Released';";

            using (MySqlCommand command = new MySqlCommand(query, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblRelease.Text = reader.GetString("released");
                    }
                }
            }
            con.Close();

            //

            con.Open();

            string query1 = "SELECT IFNULL(COUNT(*),0) as 'forwarded' FROM tracking WHERE action = 'Forwarded';";

            using (MySqlCommand command = new MySqlCommand(query1, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblForwarded.Text = reader.GetString("forwarded");
                    }
                }
            }
            con.Close();

            //
            con.Open();

            string query2 = "SELECT IFNULL(COUNT(*),0) as 'received' FROM tracking WHERE action = 'Received';";

            using (MySqlCommand command = new MySqlCommand(query2, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblAccepted.Text = reader.GetString("received");
                    }
                }
            }
            con.Close();

            con.Open();

            string query3 = "SELECT IFNULL(COUNT(*),0) as 'returned' FROM tracking WHERE action = 'Returned';";

            using (MySqlCommand command = new MySqlCommand(query3, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblReturned.Text = reader.GetString("returned");
                    }
                }
            }
            con.Close();

            con.Open();

            string query4 = "SELECT IFNULL(COUNT(*),0) as 'completed' FROM documents WHERE status = 'Done';";

            using (MySqlCommand command = new MySqlCommand(query4, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblCompleted.Text = reader.GetString("completed");
                    }
                }
            }
            con.Close();

            //
            con.Open();

            string query5 = "SELECT IFNULL(COUNT(*),0) as 'overdue' FROM documents WHERE STR_TO_DATE(dateExpired, '%Y-%m-%d') < STR_TO_DATE(CURDATE(),'%Y-%m-%d') AND status ='Processing';";

            using (MySqlCommand command = new MySqlCommand(query5, con))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblOverdue.Text = reader.GetString("overdue");
                    }
                }
            }
            con.Close();


        }

        private void chart1_Click(object sender, EventArgs e)
        {
            /*fillCharts();*/
        }

        private void lblRelease_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            fillCharts();
            label4.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {

                //Open Connection
                fillCharts();
                fillDash();

                con.Open();
                string dataTable = "select documents.docTitle as 'Document Title',unit.name as 'Unit', tracking.action as 'Action', tracking.time as 'Time' from documents, tracking, unit where tracking.documentID = documents.documentID and tracking.location = unit.unitID and DATE(date) = CURDATE();";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewRecent.DataSource = dtable;

                //disable editing cell
                dataGridViewRecent.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewRecent.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewRecent.Visible = true;
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }


            //
            
        }

        private void lblRelease_Click(object sender, EventArgs e)
        {

        }
    }
}
