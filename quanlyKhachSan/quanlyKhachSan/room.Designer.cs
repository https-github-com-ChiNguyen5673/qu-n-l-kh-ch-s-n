namespace quanlyKhachSan
{
    partial class room
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
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.ratingStar = new DevComponents.DotNetBar.Controls.RatingStar();
            this.lbRoomName = new DevComponents.DotNetBar.LabelX();
            this.lbCustomerName = new DevComponents.DotNetBar.LabelItem();
            this.lbCustomerIdcard = new DevComponents.DotNetBar.LabelItem();
            this.lbDeposits = new DevComponents.DotNetBar.LabelItem();
            this.lbBillId = new DevComponents.DotNetBar.LabelItem();
            this.lbTimeInPut = new DevComponents.DotNetBar.LabelItem();
            this.btnBookRoom = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoomStatus = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoomEmpty = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoomDirty = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoomBroken = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoomTransfer = new DevComponents.DotNetBar.ButtonItem();
            this.buttonX1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Controls.Add(this.ratingStar);
            this.buttonX1.Controls.Add(this.lbRoomName);
            this.buttonX1.Location = new System.Drawing.Point(0, 0);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.buttonX1.Size = new System.Drawing.Size(204, 127);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lbCustomerName,
            this.lbCustomerIdcard,
            this.lbDeposits,
            this.lbBillId,
            this.lbTimeInPut,
            this.btnBookRoom,
            this.btnRoomStatus,
            this.btnRoomTransfer});
            this.buttonX1.SubItemsExpandWidth = 20;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // ratingStar
            // 
            // 
            // 
            // 
            this.ratingStar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ratingStar.Location = new System.Drawing.Point(59, 98);
            this.ratingStar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ratingStar.Name = "ratingStar";
            this.ratingStar.Rating = 5;
            this.ratingStar.Size = new System.Drawing.Size(109, 28);
            this.ratingStar.TabIndex = 3;
            this.ratingStar.TextColor = System.Drawing.Color.Empty;
            // 
            // lbRoomName
            // 
            this.lbRoomName.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbRoomName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbRoomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRoomName.ForeColor = System.Drawing.Color.Green;
            this.lbRoomName.Location = new System.Drawing.Point(23, 4);
            this.lbRoomName.Margin = new System.Windows.Forms.Padding(4);
            this.lbRoomName.Name = "lbRoomName";
            this.lbRoomName.SingleLineColor = System.Drawing.Color.Transparent;
            this.lbRoomName.Size = new System.Drawing.Size(145, 27);
            this.lbRoomName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lbRoomName.Symbol = "";
            this.lbRoomName.TabIndex = 2;
            this.lbRoomName.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // lbCustomerName
            // 
            this.lbCustomerName.GlobalItem = false;
            this.lbCustomerName.Name = "lbCustomerName";
            this.lbCustomerName.Text = "Tên khách hàng :";
            // 
            // lbCustomerIdcard
            // 
            this.lbCustomerIdcard.Name = "lbCustomerIdcard";
            this.lbCustomerIdcard.Text = "CMND :16150261";
            // 
            // lbDeposits
            // 
            this.lbDeposits.GlobalItem = false;
            this.lbDeposits.Name = "lbDeposits";
            this.lbDeposits.Text = "Tiền đặt cọc : ";
            // 
            // lbBillId
            // 
            this.lbBillId.GlobalItem = false;
            this.lbBillId.Name = "lbBillId";
            this.lbBillId.Text = "Mã hóa đơn : ";
            // 
            // lbTimeInPut
            // 
            this.lbTimeInPut.GlobalItem = false;
            this.lbTimeInPut.Name = "lbTimeInPut";
            this.lbTimeInPut.Text = "Giờ vào :";
            // 
            // btnBookRoom
            // 
            this.btnBookRoom.Name = "btnBookRoom";
            this.btnBookRoom.PersonalizedMenus = DevComponents.DotNetBar.ePersonalizedMenus.Both;
            this.btnBookRoom.Text = "Đặt phòng";
            this.btnBookRoom.Click += new System.EventHandler(this.btnBookRoom_Click);
            // 
            // btnRoomStatus
            // 
            this.btnRoomStatus.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleIfRecentlyUsed;
            this.btnRoomStatus.Name = "btnRoomStatus";
            this.btnRoomStatus.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnRoomEmpty,
            this.btnRoomDirty,
            this.btnRoomBroken});
            this.btnRoomStatus.Text = "Trạng thái";
            this.btnRoomStatus.Click += new System.EventHandler(this.btnRoomStatus_Click);
            // 
            // btnRoomEmpty
            // 
            this.btnRoomEmpty.Name = "btnRoomEmpty";
            this.btnRoomEmpty.Text = "Phòng trống";
            this.btnRoomEmpty.Click += new System.EventHandler(this.btnRoomEmpty_Click);
            // 
            // btnRoomDirty
            // 
            this.btnRoomDirty.Name = "btnRoomDirty";
            this.btnRoomDirty.Text = "Phòng đang dọn";
            this.btnRoomDirty.Click += new System.EventHandler(this.btnRoomDirty_Click);
            // 
            // btnRoomBroken
            // 
            this.btnRoomBroken.Name = "btnRoomBroken";
            this.btnRoomBroken.Text = "Phòng đang sửa";
            this.btnRoomBroken.Click += new System.EventHandler(this.btnRoomBroken_Click);
            // 
            // btnRoomTransfer
            // 
            this.btnRoomTransfer.GlobalItem = false;
            this.btnRoomTransfer.Name = "btnRoomTransfer";
            this.btnRoomTransfer.Text = "Chuyển phòng";
            // 
            // room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonX1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "room";
            this.Size = new System.Drawing.Size(205, 129);
            this.buttonX1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelItem lbCustomerName;
        private DevComponents.DotNetBar.LabelItem lbCustomerIdcard;
        private DevComponents.DotNetBar.LabelX lbRoomName;
        
        private DevComponents.DotNetBar.LabelItem lbDeposits;
        private DevComponents.DotNetBar.LabelItem lbBillId;
        private DevComponents.DotNetBar.Controls.RatingStar ratingStar;
        private DevComponents.DotNetBar.ButtonItem btnBookRoom;
        private DevComponents.DotNetBar.ButtonItem btnRoomStatus;
        private DevComponents.DotNetBar.ButtonItem btnRoomEmpty;
        private DevComponents.DotNetBar.ButtonItem btnRoomDirty;
        private DevComponents.DotNetBar.ButtonItem btnRoomBroken;
        private DevComponents.DotNetBar.LabelItem lbTimeInPut;
        private DevComponents.DotNetBar.ButtonItem btnRoomTransfer;
    }
}
