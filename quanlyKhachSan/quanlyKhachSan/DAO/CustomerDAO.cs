using quanlyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyKhachSan.DAO
{
    public class CustomerDAO
    {
        QLKHACHSANEntities db = null;

        public CustomerDAO()
        {
            db = new QLKHACHSANEntities();
        }

        public List<Customer> ListOfCustomer()
        {
            var list = db.Database.SqlQuery<Customer>("EXEC dbo.USP_ShowCustomer").ToList();
            return list;           
        }
        public List<Customer> SearchCustomer(string str)
        {
            var list = db.Database.SqlQuery<Customer>("EXEC dbo.USP_SearchCustomer @name",new object[] { new SqlParameter("@name",str )}).ToList();
            return list;
        }
        public int AddCustomer(int idEmploye ,string name ,string phone , bool sex ,string idcard ,string adress,DateTime birthday)
        {
            
                object[] parameter =
                {
                    new SqlParameter("@idEmploye",idEmploye),
                    new SqlParameter("@customerName",name),
                    new SqlParameter("@phoneNumber",phone),
                    new SqlParameter("@sex",sex),
                    new SqlParameter("@identityCart",idcard),
                    new SqlParameter("@address",adress),
                    new SqlParameter("@birthDay",birthday),

                };
                int result = db.Database.ExecuteSqlCommand("EXEC  USP_AddCustomer @idEmploye ,  @customerName , @phoneNumber , @sex , @identityCart , @address , @birthDay", parameter);
                return result;
           
        }
        //kiểm tra xem đã tồn tài cmnd chưa : true đã tòn tại
        public bool ChechIDCard(string idcard)
        {
            
            var customer = db.KHACHHANGs.Where(p=>p.CMND.Equals(idcard)).FirstOrDefault();
            if (customer != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
