using quanlyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyKhachSan.DAO
{
    public class BillDAO
    {
        QLKHACHSANEntities db = null;
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BillDAO();
                return BillDAO.instance;
            }
            private set => instance = value;
        }

        public BillDAO()
        {
            db = new QLKHACHSANEntities();
        }
        public HDTHANHTOAN getBill(int idBill)
        {
            return db.HDTHANHTOANs.First(p => p.MAHD == idBill);
        }

        public HDTHANHTOAN initializationBill()
        {
            var bill = new HDTHANHTOAN();
            bill.MAKHACHHANG = null;
            bill.TIENKHACHDUA = 0;
            bill.TONGTIENPHONG = 0;
            bill.TONGTIENDICHVU = 0;
            bill.MANV = null;
            bill.NGAYHD = DateTime.Now;
            bill.TRANGTHAI = 0;
            db.HDTHANHTOANs.Add(bill);
            db.SaveChanges();
            return bill;

            //var para = new object[] { new SqlParameter("@idbill", bill.MAHD) };
            //db.Database.SqlQuery<HDTHANHTOAN>("EXEC dbo.USP_BillDelete @idbill  ", para);
            //db.SaveChanges();  

        }
        public void UpdateBill(int IbBill, int IDCustomer,int customerMoney, string allRoomName)
        {
            var bill = db.HDTHANHTOANs.First<HDTHANHTOAN>(p => p.MAHD == IbBill);
            bill.MAKHACHHANG = IDCustomer;
            bill.TIENKHACHDUA = customerMoney;
            bill.PHONGDATHUE = allRoomName;
            db.SaveChanges();
        }
        public bool removedBill(HDTHANHTOAN bill)
        {
            
                db.Database.ExecuteSqlCommand("EXEC dbo.USP_BillDelete @idBill", new object[] { new SqlParameter("@idBill", bill.MAHD) }); 
                db.SaveChanges();
                return true;
            
        }
        public bool AddDetailBookRoom(int idBill ,int idRoom,DateTime timeInput)
        {
            try
            {
                var detailBill = new CHITIETDATPHONG();
                detailBill.MAHD = idBill;
                detailBill.MAPHONG = idRoom;
                detailBill.THOIGIANDEN = timeInput;
                detailBill.THOIGIANDI = null;
                detailBill.TRANGTHAI = false;
                detailBill.GIAPHONG = 0;
                db.CHITIETDATPHONGs.Add(detailBill);
                var room = db.PHONGs.First(p => p.MAPHONG == idRoom);
                room.TRANGTHAI = 1;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<DetailRoomBill> ShowRoomSelected(int idBill)
        {
            
            return db.Database.SqlQuery<DetailRoomBill>("USP_ShowRoomSelectedByIdBill @idBill", new object[] { new SqlParameter("@idBill", idBill) }).ToList();
        }
        public void RemoveAllRoomSelected(int idBill)
        {

             db.Database.ExecuteSqlCommand("USP_DeleteAllRoomSelected @idBill", new object[] { new SqlParameter("@idBill", idBill) });
        }
        public bool CheckOutRoomBill( HDTHANHTOAN Bill , int idroom)
        {
            CHITIETDATPHONG roombill = db.CHITIETDATPHONGs.Where(p => p.MAHD == Bill.MAHD && p.MAPHONG == idroom).SingleOrDefault();
            if (roombill.TRANGTHAI == false)
            {
                
                PHONG room = db.PHONGs.Where(p => p.MAPHONG == idroom).SingleOrDefault();
                LOAIPHONG groupRoom = db.LOAIPHONGs.Where(p => p.MALOAIPHONG == room.MALOAIPHONG).SingleOrDefault();
                roombill.THOIGIANDI = DateTime.Now;
                DateTime timeStar = Convert.ToDateTime(roombill.THOIGIANDEN);
                DateTime timeEnd = Convert.ToDateTime(roombill.THOIGIANDI);
                TimeSpan time = timeEnd.Subtract(timeStar);
                if (time.Days > 0)
                {
                    roombill.GIAPHONG = time.Days * groupRoom.GIANGAY + ((int)time.Hours % 24) * groupRoom.GIAGIOSAU;
                }
                else if (time.Hours > 0)
                {
                    decimal? pice = groupRoom.GIAGIODAU + (int)(time.Hours - 1) * groupRoom.GIAGIOSAU;
                    roombill.GIAPHONG = (pice < groupRoom.GIANGAY) ? pice : groupRoom.GIANGAY;
                }
                else if (time.Hours <= 0) 
                {
                    roombill.GIAPHONG = groupRoom.GIAGIODAU;
                }
                roombill.TRANGTHAI = true;
                room.TRANGTHAI = 2;//cập nhật phòng bẩn
                db.SaveChanges();
                db.USP_UpdateTotalRoomCharge(Bill.MAHD);// cập nhật tổng tiền phòng
                db.SaveChanges();
                return true;
            }
            else
                return false;

            //roombill.GIAPHONG = time.Hours * 
        }
        public int CountRoomUnpaidOfBill(int idBill)
        {
            return db.HDTHANHTOANs.Where(p => p.MAHD == idBill).Select(p => p.CHITIETDATPHONGs.Count(c=>c.TRANGTHAI==false)).SingleOrDefault();
        }
        public void CheckOutBill(HDTHANHTOAN bill)
        {

            var Bill = db.HDTHANHTOANs.Where(p => p.MAHD == bill.MAHD).SingleOrDefault();
            Bill.NGAYTHANHTOAN = DateTime.Now;
            Bill.TRANGTHAI = 1;
            db.SaveChanges();
            List<CHITIETDATPHONG> detaiRoomBillList = db.CHITIETDATPHONGs.Where(p => p.MAHD == bill.MAHD).ToList();
            foreach (CHITIETDATPHONG item in detaiRoomBillList)
            {
                if (item.TRANGTHAI==false)
                {
                    CheckOutRoomBill(bill, item.MAPHONG);
                }
                
            }

            db.SaveChanges();
        }
        public decimal? getTotalMoneyRoomOfBill(int idBill)
        {
            return (from c in db.HDTHANHTOANs
                    where c.MAHD == idBill
                    select c.TONGTIENPHONG).SingleOrDefault();
        }
    }
}
