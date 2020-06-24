using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using quanlyKhachSan.DAO;

namespace quanlyKhachSan
{
    public partial class fRoom : DevComponents.DotNetBar.OfficeForm
    {
        QLKHACHSANEntities db = new QLKHACHSANEntities();
        public delegate void roomdelegate();
        public event roomdelegate roomrEven;

        private bool reloadRoom = false;

        BindingSource bindingRoom = new BindingSource();
        public fRoom()
        {
            InitializeComponent();
            dtgvRoom.DataSource = bindingRoom;
            loadListRoom();
            AddRoomBinding();
            loadCbBGroupRoom();
        }
        #region function
        void AddRoomBinding()
        {
            txtRoomId.DataBindings.Add(new Binding("Text", dtgvRoom.DataSource, "MAPHONG", true, DataSourceUpdateMode.Never));
            txtRoomName.DataBindings.Add(new Binding("Text", dtgvRoom.DataSource, "TENPHONG", true, DataSourceUpdateMode.Never));
            nmRoomFloor.DataBindings.Add(new Binding("Value", dtgvRoom.DataSource, "TANG", true, DataSourceUpdateMode.Never));
            nmRoomStatus.DataBindings.Add(new Binding("Value", dtgvRoom.DataSource, "TRANGTHAI", true, DataSourceUpdateMode.Never));
          
        }
        void loadListRoom()
        {
            bindingRoom.DataSource = db.USP_ViewRoom();
        }
        void loadCbBGroupRoom()
        {
            cbbGroupRoom.DataSource = db.LOAIPHONGs.Select(p=>p).ToList();
            cbbGroupRoom.DisplayMember = "TENLOAIPHONG";
            cbbGroupRoom.ValueMember = "MALOAIPHONG";
        }
        #endregion

        #region Event
        private void btnRoomView_Click(object sender, EventArgs e)
        {
            loadListRoom();
        }

        private void btnRoomAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string roomName = txtRoomName.Text;
                int roomFloor = nmRoomFloor.Value;
                int? groupRoomID = Convert.ToInt16(cbbGroupRoom.SelectedValue);
                db.USP_InsertRoom(roomName, groupRoomID, roomFloor, 0);
                loadListRoom();
                MessageBox.Show("Thêm phòng thành công !");
                reloadRoom=true;
            }
            catch
            {
                MessageBox.Show("có lỗi khi thêm ! vui lòng kiểm tra giá trị nhập vào");
            }
        }

        private void btnRoomEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int roomID = Convert.ToInt16(txtRoomId.Text);
                string roomName = txtRoomName.Text;
                int roomFloor = nmRoomFloor.Value;
                int? groupRoomID = Convert.ToInt16(cbbGroupRoom.SelectedValue);
                int roomStatus = nmRoomStatus.Value;
                db.USP_UpdateRoom(roomID, roomName, "", groupRoomID, roomFloor, roomStatus);
                MessageBox.Show("Thay đổi sản phẩm thành công !");
                loadListRoom();
                reloadRoom = true;
            }
            catch
            {
                MessageBox.Show("Có lỗi khi sửa .Vui lòng kiểm tra lại các giá trị nhập vào!");
            }

        }

        private void btnRoomRemove_Click(object sender, EventArgs e)
        {          
                int roomID = Convert.ToInt16(txtRoomId.Text);
                if (MessageBox.Show("Bạn có chắc muốn xóa phòng :" + txtRoomName.Text + " ?\nViệc này có thể sẽ dẫn đến mất dữ liệu về thông tin hóa đơn  ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    if (db.USP_DeleteRoom(roomID)==1)
                    {
                        MessageBox.Show("Xóa sản phẩm thành công !");
                        loadListRoom();
                        reloadRoom = true;

                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa !");
                    }
                }                                   
        }

        private void txtRoomId_TextChanged(object sender, EventArgs e)
        {
            if (dtgvRoom.SelectedCells.Count > 0 && dtgvRoom.SelectedCells[0].OwningRow.Cells["clGroupRoomID"].Value !=null)
            {
                int idG =Convert.ToInt16(dtgvRoom.SelectedCells[0].OwningRow.Cells["clGroupRoomID"].Value.ToString());             

                LOAIPHONG groupRoom = db.LOAIPHONGs.Where(p => p.MALOAIPHONG == idG).SingleOrDefault(); ;


                cbbGroupRoom.SelectedItem = groupRoom;

                int indexG = -1;
                int iG = 0;
                foreach (LOAIPHONG item in cbbGroupRoom.Items)
                {
                    if (item.MALOAIPHONG == groupRoom.MALOAIPHONG)
                    {
                        indexG = iG;
                        break;
                    }
                    iG++;
                }
                cbbGroupRoom.SelectedIndex = indexG;


            }
        }

        private void txtRoomSearch_TextChanged(object sender, EventArgs e)
        {
            bindingRoom.DataSource = db.USP_SearchRoom(txtRoomSearch.Text);
        }

        private void fRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (reloadRoom)
            {
                roomrEven();
                db.SaveChanges();
            }
        }
        #endregion 
    }
}