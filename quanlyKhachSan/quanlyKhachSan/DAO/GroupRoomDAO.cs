using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyKhachSan.DAO
{
    public class GroupRoomDAO
    {
        QLKHACHSANEntities db = null;
        private static GroupRoomDAO instance;

        public static GroupRoomDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new GroupRoomDAO();
                return GroupRoomDAO.instance;
            }
            private set => instance = value;
        }
        public LOAIPHONG GetGroupRoomByID(int idGroup)
        {
            return db.LOAIPHONGs.Where(p=>p.MALOAIPHONG==idGroup).SingleOrDefault();
        }
    }
}
