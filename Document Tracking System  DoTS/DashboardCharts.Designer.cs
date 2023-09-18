
namespace Document_Tracking_System__DoTS
{
    partial class DashboardCharts
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dashboardChartsInfo1 = new Document_Tracking_System__DoTS.DashboardChartsInfo();
            this.addDocument1 = new Document_Tracking_System__DoTS.AddDocument();
            this.SuspendLayout();
            // 
            // dashboardChartsInfo1
            // 
            this.dashboardChartsInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardChartsInfo1.Location = new System.Drawing.Point(0, 0);
            this.dashboardChartsInfo1.Name = "dashboardChartsInfo1";
            this.dashboardChartsInfo1.Size = new System.Drawing.Size(1354, 649);
            this.dashboardChartsInfo1.TabIndex = 0;
            // 
            // addDocument1
            // 
            this.addDocument1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addDocument1.Location = new System.Drawing.Point(0, 0);
            this.addDocument1.Name = "addDocument1";
            this.addDocument1.Size = new System.Drawing.Size(1354, 649);
            this.addDocument1.TabIndex = 1;
            // 
            // DashboardCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addDocument1);
            this.Controls.Add(this.dashboardChartsInfo1);
            this.Name = "DashboardCharts";
            this.Size = new System.Drawing.Size(1354, 649);
            this.Load += new System.EventHandler(this.DashboardCharts_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DashboardChartsInfo dashboardChartsInfo1;
        private AddDocument addDocument1;
    }
}
