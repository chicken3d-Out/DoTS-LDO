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
    public partial class Tracking : UserControl
    {
        public Tracking()
        {
            InitializeComponent();
        }

        //Initialize Database Connection
        MySqlConnection con = new MySqlConnection("server=154.49.142.154;username=u505291967_leytedivision;password=04ReactAngularVue;database=u505291967_dotsLDO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adp = new MySqlDataAdapter();


        public void trackDocument()
        {
            try
            {
                con.Close();
                //Open Connection

                con.Open();
                string dataTable = "select tracking.description as 'Description', documents.status as 'Status' from tracking, documents where tracking.documentID = documents.documentID AND documents.trackingNo = '"+txt_Search.Text+"';";
                adp = new MySqlDataAdapter(dataTable, con);
                DataTable dtable = new DataTable();
                adp.Fill(dtable);

                //fills the datagridview
                dataGridViewTrack.DataSource = dtable;



                //disable editing cell
                dataGridViewTrack.ReadOnly = true;


                if (dtable.Rows.Count == 0)
                {
                    lbl_NoData.Visible = true;
                    dataGridViewTrack.Visible = false;
                }
                else
                {
                    lbl_NoData.Visible = false;
                    dataGridViewTrack.Visible = true;
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

        private void dataGridViewTrack_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Tracking_Load(object sender, EventArgs e)
        {
            lbl_NoData.Visible = true;
            dataGridViewTrack.Visible = false;

            txt_Search.Text = "Search Tracking Number Here...";
            txt_Search.ForeColor = Color.Gray;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            trackDocument();
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
    }
}
