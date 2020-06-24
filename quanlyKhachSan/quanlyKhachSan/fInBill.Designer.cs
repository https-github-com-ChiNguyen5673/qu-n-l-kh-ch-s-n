namespace quanlyKhachSan
{
    partial class fInBill
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crvBill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvBill
            // 
            this.crvBill.ActiveViewIndex = -1;
            this.crvBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvBill.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvBill.Location = new System.Drawing.Point(0, 0);
            this.crvBill.Name = "crvBill";
            this.crvBill.Size = new System.Drawing.Size(846, 802);
            this.crvBill.TabIndex = 0;
            this.crvBill.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // fInBill
            // 
            this.ClientSize = new System.Drawing.Size(846, 802);
            this.Controls.Add(this.crvBill);
            this.Name = "fInBill";
            this.Text = "Hóa đơn thanh toán";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvBill;
    }
}