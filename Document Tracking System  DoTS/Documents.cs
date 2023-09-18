using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Document_Tracking_System__DoTS
{
    public partial class Documents : UserControl
    {
        public Documents()
        {
            InitializeComponent();
        }
        public void bringtofront()
        {
            addDocument1.BringToFront();
            /*MessageBox.Show("Hello");*/
        }

        private void Documents_Load(object sender, EventArgs e)
        {
            documentInfo1.BringToFront();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNewDocument_Click(object sender, EventArgs e)
        {
            
        }

        private void addDocument1_Load(object sender, EventArgs e)
        {

        }
    }
}
