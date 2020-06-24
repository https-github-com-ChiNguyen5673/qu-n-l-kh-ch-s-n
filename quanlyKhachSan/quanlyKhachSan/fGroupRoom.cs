using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace quanlyKhachSan
{
    public partial class fGroupRoom : DevComponents.DotNetBar.OfficeForm
    {
        QLKHACHSANEntities db = new QLKHACHSANEntities();
       

       

        BindingSource bindingGroupRoom = new BindingSource();
        public fGroupRoom()
        {
            InitializeComponent();
            dtgvGroupRoom.DataSource = bindingGroupRoom;
            loadListGroupRoom();
            AddGroupRoomBinding();
           
        }

        private void AddGroupRoomBinding()
        {
            txtGroupRoomId.DataBindings.Add(new Binding("Text", dtgvGroupRoom.DataSource, "MALOAIPHONG", true, DataSourceUpdateMode.Never));
            txtGroupRoomName.DataBindings.Add(new Binding("Text", dtgvGroupRoom.DataSource, "TENLOAIPHONG", true, DataSourceUpdateMode.Never));
            nmFirstHourPrice.DataBindings.Add(new Binding("Value", dtgvGroupRoom.DataSource, "GIAGIODAU", true, DataSourceUpdateMode.Never));
            nmLateHourPrice.DataBindings.Add(new Binding("Value", dtgvGroupRoom.DataSource, "GIAGIOSAU", true, DataSourceUpdateMode.Never));
            nmDayPrice.DataBindings.Add(new Binding("Value", dtgvGroupRoom.DataSource, "GIANGAY", true, DataSourceUpdateMode.Never));
            nmMorthPrice.DataBindings.Add(new Binding("Value", dtgvGroupRoom.DataSource, "GIATHANG", true, DataSourceUpdateMode.Never));

        }

        private void loadListGroupRoom()
        {
            bindingGroupRoom.DataSource = db.USP_ViewGroupRoom1();
        }

        private void btnGroupRoomView_Click(object sender, EventArgs e)
        {
            loadListGroupRoom();
        }

        private void btnGroupRoomAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtGroupRoomName.Text;
                int firstHourPrice = nmFirstHourPrice.Value;
                int latetHourPrice = nmLateHourPrice.Value;
                int dayPrice = nmDayPrice.Value;
                int morthPrice = nmMorthPrice.Value;

                db.USP_InsertGroupRoom(name, "", latetHourPrice, dayPrice, morthPrice, firstHourPrice);
                loadListGroupRoom();
                MessageBox.Show("Thêm loại phòng thành công !");
                
            }
            catch
            {
                MessageBox.Show("có lỗi khi thêm ! vui lòng kiểm tra giá trị nhập vào");
            }
        }

        private void btnGroupRoomEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt16(txtGroupRoomId.Text);
                string name = txtGroupRoomName.Text;
                int firstHourPrice = nmFirstHourPrice.Value;
                int latetHourPrice = nmLateHourPrice.Value;
                int dayPrice = nmDayPrice.Value;
                int morthPrice = nmMorthPrice.Value;

                db.USP_UpdateGroupRoom(id, name, "", latetHourPrice, dayPrice, morthPrice, firstHourPrice);
                loadListGroupRoom();
                MessageBox.Show("Sửa loại phòng thành công !");

            }
            catch
            {
                MessageBox.Show("có lỗi khi Sửa ! vui lòng kiểm tra giá trị nhập vào");
            }
        }

        private void btnGroupRoomRemove_Click(object sender, EventArgs e)
        {

        }

        private void txtGroupRoomSearch_TextChanged(object sender, EventArgs e)
        {
            bindingGroupRoom.DataSource = db.USP_SearchGroupRoom(txtGroupRoomSearch.Text);
        }
    }
}