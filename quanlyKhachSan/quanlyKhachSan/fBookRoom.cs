using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using quanlyKhachSan.DAO;
using quanlyKhachSan.DTO;

namespace quanlyKhachSan
{
    public partial class fBookRoom : DevComponents.DotNetBar.OfficeForm
    {
        #region testInput
        public bool IsNumber(string pText)
        {
            Regex regex = null;
            try
            {
                regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
                return regex.IsMatch(pText);
            }
            catch (Exception ex)
            {
                return regex.IsMatch(pText);
            }
        }
        #endregion
        public delegate void bookroomdelegate();
        public event bookroomdelegate bookroomrEven;

        BindingSource bindingCustomer = new BindingSource();
        private HDTHANHTOAN bill;
        public HDTHANHTOAN Bill { get => bill; set => bill = value; }

        public fBookRoom(HDTHANHTOAN bill,int amountRoom)
        {
            InitializeComponent();
            countRoomSelected = amountRoom;
            Bill = bill;

            Load();
        }

        void Load()
        {
            loadLBAddRoom();
            LoadLBRoomSelected();
            

            dtgvCustomer.DataSource = bindingCustomer;
            loadListCustomer();
            AddCustomerBinding();
            rdobtnMan.Checked = true;
            LoadDateTimePicker();

            lockUseCustomer(false);
            
        }
        void loadLBAddRoom()
        {
            var dao = new RoomDAO();
            lbRoomEmpty.DataSource = dao.SearchRoom(0);
            lbRoomEmpty.DisplayMember = "TENPHONG";
            lbRoomEmpty.ValueMember = "MAPHONG";
        }
        void loadListCustomer()
        {
            var dao = new CustomerDAO().ListOfCustomer();
            
            bindingCustomer.DataSource = dao;
            bindingCustomer.Position = bindingCustomer.Count - 1;
            dtgvCustomer.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        void AddCustomerBinding()
        {
            txtCustomerName.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "HOTEN", true, DataSourceUpdateMode.Never));
            txtCustomerID.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "MAKHACHHANG", true, DataSourceUpdateMode.Never));
            txtCustomerAddress.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "DIACHI", true, DataSourceUpdateMode.Never));
            txtCustomerIdentityCard.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "CMND", true, DataSourceUpdateMode.Never));
            txtPhoneNumber.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "SDT", true, DataSourceUpdateMode.Never));
            txtCustomerRank.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "TENPC", true, DataSourceUpdateMode.Never));
            dtpkCustomerBirthday.DataBindings.Add(new Binding("Value", dtgvCustomer.DataSource, "NGAYSINH", true, DataSourceUpdateMode.Never));
            //dtpk.DataBindings.Add(new Binding("Value", dtgvCustomer.DataSource, "NGAYSINH", true, DataSourceUpdateMode.Never));           

        }
        void LoadDateTimePicker()
        {
            dtpkInput.Value = DateTime.Now;
            
        }
        void LoadLBRoomSelected()
        {
            var dao = new BillDAO();
            LbRoomSelected.DataSource = dao.ShowRoomSelected(Bill.MAHD);
            LbRoomSelected.DisplayMember = "TENPHONG";
            LbRoomSelected.ValueMember = "MAPHONG";
        }

        void lockUseCustomer(bool e)
        {
            txtCustomerName.Enabled = e;
            txtCustomerIdentityCard.Enabled = e;
            rdobtnMan.Enabled = e;
            rdobtnWoman.Enabled = e;
            dtpkCustomerBirthday.Enabled = e;
            txtCustomerAddress.Enabled = e;
            txtPhoneNumber.Enabled = e;
            btnAddCustomer.Enabled = !e;
            btnSaveCustomer.Enabled = e;
            btnCancelCustomer.Enabled = e;

            dtgvCustomer.Enabled = !e;
            txtSearchCustomer.Enabled = !e;
            btnSearchCustomer.Enabled = !e;
            btnViewCustomer.Enabled = !e;

        }
        void clearControlCustomer()
        {
            txtCustomerName.Clear();
            txtCustomerIdentityCard.Clear();
            rdobtnMan.Checked = false;
            rdobtnWoman.Checked = false;
            dtpkCustomerBirthday.ResetText();
            txtCustomerAddress.Clear();
            txtPhoneNumber.Clear();
            

            
        }
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            if (lbRoomEmpty.SelectedValue != null)
            {
                int idRoom = Convert.ToInt32(lbRoomEmpty.SelectedValue.ToString());
                var dao = new BillDAO();
                dao.AddDetailBookRoom(Bill.MAHD, idRoom, dtpkInput.Value);
                LoadLBRoomSelected();
                loadLBAddRoom();
                countRoomSelected += 1;


            }


        }

        private void dtgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvCustomer.CurrentRow.Cells[2].Value.ToString() == "True")
            {
                rdobtnMan.Checked = true;
            }
            else
            {
                rdobtnWoman.Checked = true;
            }
        }



        private void btnViewCustomer_Click(object sender, EventArgs e)
        {
            loadListCustomer();
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            string str = txtSearchCustomer.Text;
            var dao = new CustomerDAO().SearchCustomer(str);
            bindingCustomer.DataSource = dao;
        }



        private void fBookRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saveBill)
            {


                if (MessageBox.Show("Bạn có thật sự muốn thoát ! hóa đơn sẽ không được lưu trữ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    var dao = new BillDAO();
                    dao.removedBill(Bill);


                }
                else
                    e.Cancel = true;
            }


        }

        private void btnRemoveBill_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemoveRoom_Click(object sender, EventArgs e)
        {
            var dao = new BillDAO();
            dao.RemoveAllRoomSelected(Bill.MAHD);
            LoadLBRoomSelected();
            loadLBAddRoom();
            countRoomSelected = 0;
        }

        

        

        

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            lockUseCustomer(true);
            clearControlCustomer();
        }

        private void btnCancelCustomer_Click(object sender, EventArgs e)
        {
            lockUseCustomer(false);
            lbError.Visible = false;
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                int IdEmployee = 1;
                string Name ="";
                string Idcard = "";
                bool Sex ;
                DateTime Birthday;
                string Phone = "";
                string Adress = txtCustomerAddress.Text; 
                //kiểm tra họ tên
                if (!IsNumber(txtCustomerName.Text) && txtCustomerName.Text !="")
                {
                     Name = txtCustomerName.Text;
                }
                else
                {
                    lbError.Text=("Họ tên nhập vào không đước chứa kí tự số !");
                    lbError.Visible = true;
                    txtCustomerName.Select();
                    txtCustomerName.SelectAll();
                    return;
                }

                // kiểm tra cmnd
                var ChechIDCard = new CustomerDAO().ChechIDCard(txtCustomerIdentityCard.Text);
                if (IsNumber(txtCustomerIdentityCard.Text) && txtCustomerIdentityCard.Text.Length > 8 && !ChechIDCard)
                {
                     Idcard = txtCustomerIdentityCard.Text;
                }
                else
                {
                    lbError.Text = ("CMND nhập vào không hợp lệ hoặc đã tồn tại !");
                    lbError.Visible = true;
                    txtCustomerIdentityCard.Select();
                    txtCustomerIdentityCard.SelectAll();
                    return;
                }
                //kiểm tra giớ tính
                if ((rdobtnMan.Checked == true) || (rdobtnWoman.Checked == true))
                {
                     Sex = (rdobtnMan.Checked == true) ? true : false;
                }
                else
                {
                    lbError.Text = ("Bạn chưa chọn giới tính !");
                    lbError.Visible = true;
                    return;
                }
                //kiểm tra ngày sinh
                if (dtpkCustomerBirthday.Value <= DateTime.Now || dtpkCustomerBirthday.Value ==null)
                {
                     Birthday = dtpkCustomerBirthday.Value;
                }
                else
                {
                    lbError.Text = ("Ngày sinh nhập vào không hợp lệ !");
                    lbError.Visible = true;
                    return;
                }
                //kiểm ra phone
                if (txtPhoneNumber.Text=="" || (IsNumber(txtPhoneNumber.Text) && txtCustomerIdentityCard.Text.Length > 8))
                {
                     Phone = txtPhoneNumber.Text;
                }
                else
                {
                    lbError.Text = ("SDT nhập vào không hợp lệ !");
                    lbError.Visible = true;
                    txtPhoneNumber.Select();
                    txtPhoneNumber.SelectAll();
                    return;
                }
                var dao = new CustomerDAO();
                if (dao.AddCustomer(IdEmployee, Name, Phone,Sex,Idcard,Adress,Birthday)>0)
                {
                    MessageBox.Show("Thêm khách hàng thành công !", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    lbError.Visible = false;
                    loadListCustomer();
                    lockUseCustomer(false);
                    dtgvCustomer.Rows[dtgvCustomer.RowCount - 2].Selected = true;
                    bindingCustomer.Position = bindingCustomer.Count-1;
                }


            }
            catch
            {
                lbError.Text = ("Có lỗi ! vui lòng kiểm tra lại giá trị nhập vào !");
                lbError.Visible = true;
            }


        }

        private bool saveBill = false;
        private int countRoomSelected = 0;
        private void btnAddBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (countRoomSelected != 0)
                {
                    var dao = new BillDAO();
                    string allRoomName = "";
                    foreach (DetailRoomBill item in dao.ShowRoomSelected(Bill.MAHD))
                    {
                        allRoomName += item.TENPHONG +" , ";
                    }
                    dao.UpdateBill(Bill.MAHD, (int)dtgvCustomer.CurrentRow.Cells[0].Value, (int)nmrUPCustomerMoney.Value,allRoomName);
                    
                    saveBill = true;
                    bookroomrEven();
                    this.Close();// draw.io
                }
                else
                    MessageBox.Show("Bạn chưa chọn phòng !");
            }
            catch
            {
                MessageBox.Show("Có lỗi . vui lòng kiểm tra lại dữ liệu đầu vào !");
            }
        }
    }

}