using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using quanlyKhachSan.DAO;
using quanlyKhachSan.DTO;

namespace quanlyKhachSan
{
    public partial class fBill : DevComponents.DotNetBar.OfficeForm
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();



        QLKHACHSANEntities db = new QLKHACHSANEntities();

        public HDTHANHTOAN isBill { get; set; }
        private KHACHHANG customerBill = new KHACHHANG();
        
        public KHACHHANG CustomerBill { get => customerBill; set => customerBill = value; }

        public delegate void Billdelegate();
        public event Billdelegate billEven;

        public fBill(HDTHANHTOAN bill,KHACHHANG customer)
        {
            InitializeComponent();
            isBill = bill;
            CustomerBill = customer;
            Load();
           
        }
        

        void Load()
        {
            LoadServiceList();
            loadDetailRoomBill();
            loadCustomer();
            loadServiceUsed();
            loadBillInformation();
            billChecked(isBill.TRANGTHAI);
        }
        void billChecked(int? statusBill)
        {
            if (statusBill==1)
            {
                dtgvService.Enabled = false;
                dtgvServiceUsed.Enabled = false;
                dtgvDetailRoomBill.Enabled = false;
                btnChechOut.Enabled = false;
            }
           
        }
        void LoadServiceList()
        {
            var dao = new ServiceDAO();
            dtgvService.DataSource = db.GetAllServiceList();
            
        }
        void loadServiceUsed()
        {
            
            var dao = new ServiceDAO();
            dtgvServiceUsed.DataSource = dao.listViewServiceBill(isBill.MAHD);
            


            //System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");

            //txtServiceMoney.Text = String.Format(culture, "{0:N0} vnđ", TotalPrice);
            //txtServiceMoney.Select(txtServiceMoney.Text.Length, 0);
            ////Thread.CurrentThread.CurrentCulture = culture;



        }
        void loadDetailRoomBill()
        {
            dtgvDetailRoomBill.DataSource = db.USP_ShowRoomSelectedByIdBill2(isBill.MAHD);
            
        }
        void loadCustomer()
        {
            txtCustomerName.Text = CustomerBill.HOTEN;
            txtCustomerIdcard.Text = CustomerBill.CMND;
            txtCustomerPhone.Text = CustomerBill.SDT;
            txtCustomerAddress.Text = CustomerBill.DIACHI;
            
        }
        void loadBillInformation()
        {
            txtIdBill.Text = isBill.MAHD.ToString();
            var totalServiceMoney = isBill.TONGTIENDICHVU;
            txtServiceMoney.Text = (totalServiceMoney!=null)? totalServiceMoney.Value.ToString("N0") : "0";
            txtRoomMoney.Text = isBill.TONGTIENPHONG.Value.ToString("N0");
        }

        private void dtgvService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                int idService = (int)dtgvService.CurrentRow.Cells["ColumnID"].Value;
                var dao = new ServiceDAO();
                dao.AddDetailServiceBill(isBill.MAHD, idService);
                db.USP_UpdateTotalService(isBill.MAHD);
                isBill.TONGTIENDICHVU += (decimal?)dtgvService.CurrentRow.Cells["clServicePrice"].Value;
                loadBillInformation();
                loadServiceUsed();
                





            }
            
        }

      

        private void dtgvDetailRoomBill_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtSearchService_TextChanged(object sender, EventArgs e)
        {
            
            dtgvService.DataSource = db.GetSearchServiceList(txtSearchService.Text); 
        }

        private void dtgvServiceUsed_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                int idServiceUsed = (int)dtgvServiceUsed.CurrentRow.Cells["clServiceUserID"].Value;
                var dao = new ServiceDAO();
                try
                {
                    dao.RemoveDetailServiceBill(isBill.MAHD, idServiceUsed);
                    db.USP_UpdateTotalService(isBill.MAHD);
                    isBill.TONGTIENDICHVU -= (decimal?)dtgvServiceUsed.CurrentRow.Cells["clServiceSelectedPrice"].Value;
                    loadServiceUsed();
                    loadBillInformation();
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi giảm !");

                }



            }
        }

        private void dtgvDetailRoomBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                int idRoom = (int)dtgvDetailRoomBill.CurrentRow.Cells["clRomID"].Value;
                var dao = new BillDAO();
                try
                {
                    if (dao.CountRoomUnpaidOfBill(isBill.MAHD) == 1)
                    {
                        btnChechOut.PerformClick();
                    }
                    else if(!dao.CheckOutRoomBill(isBill, idRoom))
                    {
                        MessageBox.Show("Phòng náy đã tính tiền trước đó !");
                    }
                    else
                    {
                        db.USP_UpdateTotalRoomCharge(isBill.MAHD);

                        isBill.TONGTIENPHONG = BillDAO.Instance.getTotalMoneyRoomOfBill(isBill.MAHD);

                        loadDetailRoomBill();
                        
                        loadBillInformation();
                    }




                }
                catch
                {
                    MessageBox.Show("Có lỗi khi thanh toán !");

                }



            }
        }

        private void btnUot_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChechOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thanh toán tất cả các phòng ! ", "Chú ý", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                var dao = new BillDAO();
                dao.CheckOutBill(isBill);
                billChecked(1);
                isBill.TRANGTHAI = 1;
                isBill.TONGTIENPHONG = BillDAO.Instance.getTotalMoneyRoomOfBill(isBill.MAHD);
                loadDetailRoomBill();
                loadBillInformation();
                


            }
        }

        private void btnInBill_Click(object sender, EventArgs e)
        {
            if (isBill.TRANGTHAI == 1)
            {
                SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-L78F349\SQLEXPRESS;Initial Catalog=QLKHACHSAN;Integrated Security=True");
                connection.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.USP_InBill @idBill =" + isBill.MAHD, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataSet dsBill = new DataSet();
                adapter.Fill(dsBill);
                connection.Close();

                fInBill FInBill = new fInBill(isBill.MAHD, dsBill);
                FInBill.ShowDialog();
            }
            else
                MessageBox.Show("Hóa đơn này chưa được thanh toán !");
        }

        private void fBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            billEven();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void pnTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}