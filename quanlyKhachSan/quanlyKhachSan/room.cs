using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlyKhachSan.DAO;

namespace quanlyKhachSan
{
    public partial class room : UserControl
    {
        QLKHACHSANEntities db = new QLKHACHSANEntities();
        
        public delegate void Billdelegate();
        public event Billdelegate roomEven;
        
       

        private PHONG isRoom;
        private HDTHANHTOAN billOfRoom;
        private CHITIETDATPHONG detailBillOfRoom;
        private KHACHHANG customerOfRoom;

        public PHONG IsRoom { get => isRoom; set => isRoom = value; }
        public HDTHANHTOAN BillOfRoom { get => billOfRoom; set => billOfRoom = value; }
        public CHITIETDATPHONG DetailBillOfRoom { get => detailBillOfRoom; set => detailBillOfRoom = value; }
        public KHACHHANG CustomerOfRoom { get => customerOfRoom; set => customerOfRoom = value; }

        public room(PHONG Room)
        {
            InitializeComponent();
            IsRoom = Room;
            
            load();
        }
        void Data(int idRoom)
        {
            DetailBillOfRoom = db.CHITIETDATPHONGs.SqlQuery("EXEC dbo.USP_GetDetalBill @idRoom ="+ idRoom).SingleOrDefault();
            BillOfRoom = db.HDTHANHTOANs.SqlQuery("EXEC dbo.USP_GetBill @idBill =" + DetailBillOfRoom.MAHD).SingleOrDefault();
            CustomerOfRoom = db.KHACHHANGs.SqlQuery("EXEC dbo.USP_GetCustomer @idCustomer =" + BillOfRoom.MAKHACHHANG).SingleOrDefault();
        }
        void load()
        {
            lockUserToUseTheControl(IsRoom.TRANGTHAI);
            lbRoomName.Text = IsRoom.TENPHONG;
            //status
            if (IsRoom.TRANGTHAI == 0)
            {
                buttonX1.Image = global::quanlyKhachSan.Properties.Resources._0;
                lbRoomName.ForeColor = System.Drawing.Color.Green;
            }
            else if (IsRoom.TRANGTHAI == 1)
            {
                buttonX1.Image = global::quanlyKhachSan.Properties.Resources._1;
                lbRoomName.ForeColor = System.Drawing.Color.Red;
                Data(isRoom.MAPHONG);
                lbCustomerName.Text = "Tên khách hàng : " + CustomerOfRoom.HOTEN;
                lbCustomerIdcard.Text = "SĐT : " + CustomerOfRoom.SDT;
                lbDeposits.Text = "Tiền đặt cọc : " + BillOfRoom.TIENKHACHDUA;
                lbBillId.Text = "Mã hóa đơn : " + BillOfRoom.MAHD;
                lbTimeInPut.Text ="Giờ vào : " +DetailBillOfRoom.THOIGIANDEN.Value.ToString("dd-MM-yyyy HH:mm:ss"); 
                
                

            }
            else if (IsRoom.TRANGTHAI == 2)
            {
                buttonX1.Image = global::quanlyKhachSan.Properties.Resources._3;
                lbRoomName.ForeColor = System.Drawing.Color.Olive;
                btnBookRoom.Enabled = false;
            }
            else if (IsRoom.TRANGTHAI == 3)
            {
                buttonX1.Image = global::quanlyKhachSan.Properties.Resources._5;
                lbRoomName.ForeColor = System.Drawing.Color.BlueViolet;
                btnBookRoom.Enabled = false;
            }
            //KindOfRoom
            ratingStar.NumberOfStars = Convert.ToInt16(IsRoom.MALOAIPHONG);

            


        }
        public void transferRoom(int status)
        {
            db.USP_UpdateRoomStatus(IsRoom.MAPHONG, status);
            IsRoom.TRANGTHAI = status;
            load();
        }
        void  lockUserToUseTheControl(int? roomStatus)
        {
            bool e = true;
            if (roomStatus == 1)
            {
                lbCustomerName.Visible = e;
                lbCustomerIdcard.Visible = e;
                lbTimeInPut.Visible = e;
                lbDeposits.Visible = e;
                lbBillId.Visible = e;
                btnBookRoom.Enabled = !e;
                btnRoomStatus.Enabled = !e;
                btnRoomTransfer.Enabled = e;
            }
            else
            {
                e = false;
                lbCustomerName.Visible = e;
                lbCustomerIdcard.Visible = e;
                lbTimeInPut.Visible = e;
                lbDeposits.Visible = e;
                lbBillId.Visible = e;
                btnBookRoom.Enabled = !e;
                btnRoomStatus.Enabled = !e;
                btnRoomTransfer.Enabled = e;
            }
            
        }



        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (isRoom.TRANGTHAI == 1)
            {
                fBill FBill = new fBill(BillOfRoom, CustomerOfRoom);
                FBill.billEven += FBillEven;
                FBill.ShowDialog();
                
            }
        }
        private void FBillEven()
        {
            roomEven();
        }

        private void btnRoomEmpty_Click(object sender, EventArgs e)
        {
            transferRoom(0);
        }

        private void btnRoomDirty_Click(object sender, EventArgs e)
        {
            transferRoom(2);
        }

        private void btnRoomBroken_Click(object sender, EventArgs e)
        {
            transferRoom(3);
        }

        private void btnBookRoom_Click(object sender, EventArgs e)
        {          
            var bill = BillDAO.Instance.initializationBill();
            BillDAO.Instance.AddDetailBookRoom(bill.MAHD, IsRoom.MAPHONG, DateTime.Now);
            var fBookRoom = new fBookRoom(bill,1);
            fBookRoom.bookroomrEven += FBillEven;
            fBookRoom.ShowDialog();
        }

        private void btnRoomStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
