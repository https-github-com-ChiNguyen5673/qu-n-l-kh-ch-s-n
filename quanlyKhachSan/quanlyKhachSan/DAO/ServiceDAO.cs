using quanlyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyKhachSan.DAO
{
    public class ServiceDAO
    {
        QLKHACHSANEntities db = null;

        public ServiceDAO()
        {
            db = new QLKHACHSANEntities();
        }
        public List<DICHVU> listService()
        {
            return db.DICHVUs.Select(p => p).ToList();
        }
        public bool AddDetailServiceBill(int idBill, int idService )
        {
            try
            {
                CHITIETSUDUNGDV detailServiceBill = db.CHITIETSUDUNGDVs.Where(p => p.MAHD == idBill && p.MADICHVU == idService).FirstOrDefault();
                decimal? pice = db.DICHVUs.Where(p => p.MADICHVU == idService).Select(p => p.GIADV).SingleOrDefault();
                if (detailServiceBill==null)//chua co chi tiet
                {

                    detailServiceBill = new CHITIETSUDUNGDV();
                    detailServiceBill.MAHD = idBill;
                    detailServiceBill.MADICHVU = idService;
                    detailServiceBill.SOLUONG = 1;
                    detailServiceBill.THANHTIEN = pice;
                    db.CHITIETSUDUNGDVs.Add(detailServiceBill);                 
                    db.SaveChanges();
                }
                else
                {
                    detailServiceBill.SOLUONG += 1;
                    detailServiceBill.THANHTIEN = detailServiceBill.SOLUONG * pice;
                    db.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveDetailServiceBill(int idBill, int idService)
        {
            try
            {
                CHITIETSUDUNGDV detailServiceBill = db.CHITIETSUDUNGDVs.Where(p => p.MAHD == idBill && p.MADICHVU == idService).FirstOrDefault();
                decimal? pice = db.DICHVUs.Where(p => p.MADICHVU == idService).Select(p => p.GIADV).SingleOrDefault();
                detailServiceBill.SOLUONG -= 1;
                if (detailServiceBill.SOLUONG > 0)
                {
                    detailServiceBill.THANHTIEN = detailServiceBill.SOLUONG * pice;
                }
                else
                {
                    db.CHITIETSUDUNGDVs.Remove(detailServiceBill);
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<ListViewServiceBill> listViewServiceBill(int idBill)
        {
            return db.Database.SqlQuery<ListViewServiceBill>("EXEC dbo.USP_ShowDetailServiceUsed @idBill ="+idBill).ToList();
        }
        public List<DICHVU> SearchService(string strname)
        {
            var list = db.DICHVUs.Where(p => p.TENDICHVU.Equals(strname)).ToList();
            return list;
        }
    }
}
