using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace quanlyKhachSan
{
    public partial class fRoomItem : DevComponents.DotNetBar.OfficeForm
    {
        QLKHACHSANEntities db = new QLKHACHSANEntities();
        BindingSource bindingRoomItem = new BindingSource();
        public fRoomItem()
        {
            InitializeComponent();
            dtgvRoomItem.DataSource = bindingRoomItem;
            loadlistItem(1);
            AddRoomItemBinding();
            loadListRoom();
            loadcbbRoom();   
            
        }

        void AddRoomItemBinding()
        {
            txtRoomItemId.DataBindings.Add(new Binding("Text", dtgvRoomItem.DataSource, "MAVATDUNG", true, DataSourceUpdateMode.Never));
            txtRoomItemName.DataBindings.Add(new Binding("Text", dtgvRoomItem.DataSource, "TENVATDUNG", true, DataSourceUpdateMode.Never));
            txtRoomItemManufature.DataBindings.Add(new Binding("Text", dtgvRoomItem.DataSource, "HANGSANXUAT", true, DataSourceUpdateMode.Never));
            txtRoomItemDetal.DataBindings.Add(new Binding("Text", dtgvRoomItem.DataSource, "CHITIET", true, DataSourceUpdateMode.Never));
            nmRoomItemPrice.DataBindings.Add(new Binding("Value", dtgvRoomItem.DataSource, "GIA", true, DataSourceUpdateMode.Never));
            dtpkRoomItemSateTime.DataBindings.Add(new Binding("Value", dtgvRoomItem.DataSource, "THOIGIANMUA", true, DataSourceUpdateMode.Never));

        }

        void loadListRoom()
        {
            lbRoom.DataSource = db.PHONGs.Select(p => p).ToList();
            lbRoom.DisplayMember = "TENPHONG";
            lbRoom.ValueMember = "MAPHONG";
        }
        void loadcbbRoom()
        {
            cbbRoom.DataSource = db.PHONGs.ToList();
            cbbRoom.DisplayMember = "TENPHONG";
            cbbRoom.ValueMember = "MAPHONG";
        }
        void loadlistItem(int idRoom)
        {
            bindingRoomItem.DataSource = db.USP_ViewRoomItemOfRoom(idRoom);
        }
        private void lbRoom_ItemClick(object sender, EventArgs e)
        {
            grRoomItem.Text ="Vật dụng - "+(lbRoom.SelectedItem as PHONG).TENPHONG.ToString();
            loadlistItem((int)lbRoom.SelectedValue);
        }

        private void txtRoomItemId_TextChanged(object sender, EventArgs e)
        {
            try
            {


                int idG = Convert.ToInt16(dtgvRoomItem.SelectedCells[0].OwningRow.Cells["clRoomID"].Value.ToString());
                PHONG Room = db.PHONGs.Where(p => p.MAPHONG == idG).SingleOrDefault(); ;
                cbbRoom.SelectedItem = Room;

                int indexG = -1;
                int iG = 0;
                foreach (PHONG item in cbbRoom.Items)
                {
                    if (item.MAPHONG == Room.MAPHONG)
                    {
                        indexG = iG;
                        break;
                    }
                    iG++;
                }
                cbbRoom.SelectedIndex = indexG;
            }
            catch
            {

            }
        }

        private void btnRoomItemAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtRoomItemName.Text;
                int roomId = (int)cbbRoom.SelectedValue;
                string manufature = txtRoomItemManufature.Text;
                decimal price = nmRoomItemPrice.Value;
                string detail = txtRoomItemDetal.Text;
                DateTime sateDay = dtpkRoomItemSateTime.Value;
                db.USP_InsertRoomItem(name,manufature,sateDay,detail,roomId,price);
                loadlistItem((int)lbRoom.SelectedValue);
                MessageBox.Show("Thêm thành công !");
                
            }
            catch
            {
                MessageBox.Show("có lỗi khi thêm ! vui lòng kiểm tra giá trị nhập vào");
            }
        }
    }
}