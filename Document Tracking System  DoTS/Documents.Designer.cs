
namespace Document_Tracking_System__DoTS
{
    partial class Documents
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
            this.documentInfo1 = new Document_Tracking_System__DoTS.DocumentInfo();
            this.addDocument1 = new Document_Tracking_System__DoTS.AddDocument();
            this.SuspendLayout();
            // 
            // documentInfo1
            // 
            this.documentInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentInfo1.Location = new System.Drawing.Point(0, 0);
            this.documentInfo1.Name = "documentInfo1";
            this.documentInfo1.Size = new System.Drawing.Size(1354, 649);
            this.documentInfo1.TabIndex = 0;
            // 
            // addDocument1
            // 
            this.addDocument1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addDocument1.Location = new System.Drawing.Point(0, 0);
            this.addDocument1.Name = "addDocument1";
            this.addDocument1.Size = new System.Drawing.Size(1354, 649);
            this.addDocument1.TabIndex = 1;
            this.addDocument1.Load += new System.EventHandler(this.addDocument1_Load);
            // 
            // Documents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addDocument1);
            this.Controls.Add(this.documentInfo1);
            this.Name = "Documents";
            this.Size = new System.Drawing.Size(1354, 649);
            this.Load += new System.EventHandler(this.Documents_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public DocumentInfo documentInfo1;
        public AddDocument addDocument1;
    }
}
