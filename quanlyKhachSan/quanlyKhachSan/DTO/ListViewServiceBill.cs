using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyKhachSan.DTO
{
    public class ListViewServiceBill
    {
        public Nullable<int> MADICHVU { get; set; }
        public string TENDICHVU { get; set; }      
        public string DONVI { get; set; }
        public Nullable<decimal> GIADV { get; set; }
        public Nullable<int> SOLUONG { get; set; }
        public Nullable<decimal> THANHTIEN { get; set; }
    }
}
