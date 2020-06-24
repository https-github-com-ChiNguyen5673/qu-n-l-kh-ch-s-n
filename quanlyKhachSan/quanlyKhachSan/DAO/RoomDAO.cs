using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyKhachSan.DAO
{
    public class RoomDAO
    {
        QLKHACHSANEntities db = null;
        private static RoomDAO instance;

        public static RoomDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new RoomDAO();
                return RoomDAO.instance;
            }
            private set => instance = value;
        }

        public RoomDAO()
        {
            db = new QLKHACHSANEntities();
        }

        public List<PHONG> listRoom()
        {
            return db.PHONGs.Select(p=>p).ToList();
        }
        public List<PHONG> SearchRoom(int status)
        {
            return db.PHONGs.Select(p => p).Where(p=>p.TRANGTHAI==status).ToList();
        }
        public List<PHONG> listRoomWithStatus (int status)
        {
            return db.PHONGs.Where(p => p.TRANGTHAI == status).ToList();
        }
    }
}
