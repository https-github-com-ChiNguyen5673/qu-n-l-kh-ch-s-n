using DevComponents.DotNetBar;
using quanlyKhachSan.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlyKhachSan
{
    public partial class Form1 : DevComponents.DotNetBar.Office2007Form
    {
        QLKHACHSANEntities db = new QLKHACHSANEntities();
        public delegate void Maindelegate();
        public event Maindelegate transferRoomEven;

        TAIKHOAN User { get; set; }
        public Form1(TAIKHOAN user)
        {
            InitializeComponent();
            User = user;
            this.SetStyle(ControlStyles.DoubleBuffer |
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint,
              true);
            this.UpdateStyles();
            loadAllRoom();
            //timer1.Start();

            
            LoadDateTimePickerBill();
            btnStatistics.PerformClick();




        }



        void loadAllRoom()
        {
            var dao = new RoomDAO();
            var listRoom = dao.listRoom();
            loadRoom(listRoom);
        }
        public List<room> Rooms { get; set; }
        void loadRoom(List<PHONG> listRoom)
        {
            flpRoom1.Controls.Clear();
            flpRoom2.Controls.Clear();
            flpRoom3.Controls.Clear();
            flpRoom4.Controls.Clear();
            flpRoom5.Controls.Clear();
            Rooms = new List<room>();
            foreach (PHONG item in listRoom)
            {
                
                room ucroom = new room(item);
                ucroom.Name = "uc" + item.MAPHONG;
                
                ucroom.roomEven +=ReloadRoom;
                if (item.TANG == 1)
                {
                    flpRoom1.Controls.Add(ucroom);
                }
                else if (item.TANG == 2)
                {
                    flpRoom2.Controls.Add(ucroom);
                }
                else if (item.TANG == 3)
                {
                    flpRoom3.Controls.Add(ucroom);
                }
                else if (item.TANG == 4)
                {
                    flpRoom4.Controls.Add(ucroom);
                }
                else if (item.TANG == 5)
                {
                    flpRoom5.Controls.Add(ucroom);
                }
                Rooms.Add(ucroom);
            }
        }
        void loadBill()
        {
            dtgvBill.DataSource = db.USP_ShowBill1();
            
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void loadBillByDate(DateTime fromDate , DateTime toDate)
        {
            dtgvBill.DataSource = db.USP_ShowBillByDate(fromDate, toDate);

        }
        void loadcbbTransferRoom()
        {
            cbbFromRoom.DataSource = RoomDAO.Instance.listRoomWithStatus(1);
            cbbFromRoom.DisplayMember = "TENPHONG";
            cbbFromRoom.ValueMember = "MAPHONG";
            cbbToRoom.DataSource = RoomDAO.Instance.listRoomWithStatus(0);
            cbbToRoom.DisplayMember = "TENPHONG";
            cbbToRoom.ValueMember = "MAPHONG";
            

        }

        private void btnEmptyRoom_MouseClick(object sender, MouseEventArgs e)
        {
            if (btnEmptyRoom.Value == false)
            {
                btnRoomHasBeenRented.Value = false;
                btnRoomClearUp.Value = false;
                btnRoomFixing.Value = false;

                var dao = new RoomDAO();
                var listRoom = dao.SearchRoom(0);
                loadRoom(listRoom);
            }
            else
            {
                loadAllRoom();
            }
        }

        private void btnRoomHasBeenRented_MouseClick(object sender, MouseEventArgs e)
        {
            if (btnRoomHasBeenRented.Value == false)
            {
                btnRoomClearUp.Value = false;
                btnEmptyRoom.Value = false;
                btnRoomFixing.Value = false;
                var dao = new RoomDAO();
                var listRoom = dao.SearchRoom(1);
                loadRoom(listRoom);
            }
            else
            {
                loadAllRoom();
            }
        }
        private void btnRoomClearUp_MouseClick(object sender, MouseEventArgs e)
        {
            if (btnRoomClearUp.Value==false)
            {
                btnRoomHasBeenRented.Value = false;
                btnEmptyRoom.Value = false;
                btnRoomFixing.Value = false;
                var dao = new RoomDAO();
                var listRoom = dao.SearchRoom(2);
                loadRoom(listRoom);
            }
            else
            {
                loadAllRoom(); 
            }
        }


        private void btnRoomFixing_MouseClick(object sender, MouseEventArgs e)
        {
            if (btnRoomFixing.Value == false)
            {
                btnRoomHasBeenRented.Value = false;
                btnRoomClearUp.Value = false;
                btnEmptyRoom.Value = false;
                var dao = new RoomDAO();
                var listRoom = dao.SearchRoom(3);
                loadRoom(listRoom);
            }
            else
            {
                loadAllRoom();
            }
        }

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            var dao = new BillDAO();
            var bill = dao.initializationBill();
            var fBookRoom = new fBookRoom(bill,0);
            fBookRoom.bookroomrEven += FbookroomEven;
            fBookRoom.ShowDialog();
        }
        private void FbookroomEven()
        {
            loadAllRoom();
            btnStatistics.PerformClick();
        }

        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
             
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                int idBill = (int)dtgvBill.CurrentRow.Cells["clBillID"].Value;
                           
                string CustomerName = dtgvBill.CurrentRow.Cells["clCustomerIdCard"].Value.ToString();
                HDTHANHTOAN BillOfRoom = db.HDTHANHTOANs.SqlQuery("EXEC dbo.USP_GetBill @idBill =" + idBill).SingleOrDefault();
                KHACHHANG CustomerOfRoom = db.KHACHHANGs.SqlQuery("EXEC dbo.USP_GetCustomer @idCustomer =" + BillOfRoom.MAKHACHHANG).SingleOrDefault();
                fBill FBill = new fBill(BillOfRoom, CustomerOfRoom);
                FBill.billEven += ReloadRoom;
                FBill.ShowDialog();


            }
        }
        private void ReloadRoom()
        {
            loadAllRoom();
            btnStatistics.PerformClick();
            loadcbbTransferRoom();
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            fRoom f = new fRoom();
            f.roomrEven += ReloadRoom;
            f.ShowDialog();
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            fService f = new fService();
            
            f.ShowDialog();
        }

        private void btnGroupRoom_Click(object sender, EventArgs e)
        {
            fGroupRoom f = new fGroupRoom();

            f.ShowDialog();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            TabControlMain.SelectedTabIndex = 1; 
        }

        private void btnRoomTransfor_Click(object sender, EventArgs e)
        {
            int idFromRoom = Convert.ToInt16(cbbFromRoom.SelectedValue);
            int idToRoom = Convert.ToInt16(cbbToRoom.SelectedValue);          
            db.USP_TraserRoomIdInDetailBill(idFromRoom, idToRoom);
            int endforeach = 0;
            foreach (room ucroom in Rooms)
            {
                if (ucroom.Name == "uc"+cbbFromRoom.SelectedValue)
                {
                    ucroom.transferRoom(2);
                    endforeach++;
                }
                else if (ucroom.Name == "uc" + cbbToRoom.SelectedValue)
                {
                    ucroom.transferRoom(1);
                    endforeach++;
                }
                if (endforeach==2)
                {
                    break;
                }
            }
            pnTransferRoom.Visible = false;
            rbbTransfer.Visible = true;
        }

       

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            rbbTransfer.Visible = false;
            loadcbbTransferRoom();
            pnTransferRoom.Visible = true;
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            loadBillByDate(dtpkFromDate.Value,dtpkToDate.Value);
            decimal? totalMoney = db.HDTHANHTOANs.Where(p => p.TRANGTHAI == 1 && p.NGAYHD >= dtpkFromDate.Value && p.NGAYHD <= dtpkToDate.Value).Sum(p=>p.TONGTIENDICHVU+p.TONGTIENPHONG);
            lbtotalMoney.Text = (totalMoney!=null)? totalMoney.Value.ToString("N0")+" (VNĐ)":"0" + " (VNĐ)";
        }

        private void btnFace_Click(object sender, EventArgs e)
        {
            fFace F = new fFace();
            F.ShowDialog();  
        }

        private void btnRoomItem_Click(object sender, EventArgs e)
        {

        }
    }
}
