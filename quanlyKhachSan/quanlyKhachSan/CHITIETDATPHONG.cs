//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace quanlyKhachSan
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHITIETDATPHONG
    {
        public int MAHD { get; set; }
        public int MAPHONG { get; set; }
        public Nullable<System.DateTime> THOIGIANDEN { get; set; }
        public Nullable<System.DateTime> THOIGIANDI { get; set; }
        public Nullable<bool> TRANGTHAI { get; set; }
        public Nullable<decimal> GIAPHONG { get; set; }
    
        public virtual PHONG PHONG { get; set; }
        public virtual HDTHANHTOAN HDTHANHTOAN { get; set; }
    }
}
