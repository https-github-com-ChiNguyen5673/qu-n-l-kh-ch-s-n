using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyKhachSan.DTO
{
    public class Customer
    {
        [DisplayName("Mã")]
        public int MAKHACHHANG { get; set; }

        [DisplayName( "Họ tên")]
        public string HOTEN { get; set; }

        [DisplayName( "Giới tính")]        
        public Boolean GIOITINH { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? NGAYSINH { get; set; } 

        public string CMND { get; set; }

        public string SDT { get; set; }

        [DisplayName( "Địa chỉ")]
        public string DIACHI { get; set; }     

        [DisplayName( "Hạng")]
        public string TENPC { get; set; }
        //private int id;
        //private string name;
        //private string cmnd;
        //private string phone;
        //private string address;
        //private string classify;

        //public int Id { get => id; set => id = value; }
        //public string Name { get => name; set => name = value; }
        //public string Cmnd { get => cmnd; set => cmnd = value; }
        //public string Phone { get => phone; set => phone = value; }
        //public string Address { get => address; set => address = value; }
        //public string Classify { get => classify; set => classify = value; }

        //public Customer(int id ,string name , string cmnd , string phone ,string address , string classify)
        //{
        //    this.Id = id ;
        //    this.Name = name;
        //    this.Cmnd = cmnd;
        //    this.Phone = phone;
        //    this.Address = address;
        //    this.Classify = classify;

        //}
        //public Customer(DataRow row)
        //{
        //    this.Id = (int)row["MAKHACHHANG"];
        //    this.Name = row["HOTEN"].ToString();
        //    this.Cmnd = row["CMND"].ToString();
        //    this.Phone = row["SDT"].ToString();
        //    this.Address = row["DIACHI"].ToString();
        //    this.Classify = row["TENPC"].ToString();

        //}
    }
}
