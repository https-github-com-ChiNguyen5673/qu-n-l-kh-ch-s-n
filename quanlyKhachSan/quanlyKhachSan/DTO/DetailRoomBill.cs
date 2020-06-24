using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyKhachSan.DTO
{
    public class DetailRoomBill
    {
        public int MAHD { get; set; }

        public int MAPHONG { get; set; }

        public string TENPHONG { get; set; }

        public DateTime THOIGIANDEN { get; set; }

        public DateTime? THOIGIANDI { get; set; }

        public bool TRANGTHAI { get; set; }
    }
}
