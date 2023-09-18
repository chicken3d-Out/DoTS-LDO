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
    public partial class DashboardCharts : UserControl
    {
        public DashboardCharts()
        {
            InitializeComponent();

            
        }

        private void DashboardCharts_Load(object sender, EventArgs e)
        {
            dashboardChartsInfo1.BringToFront();
        }

       
    }
}
