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
    
    public partial class HDTHANHTOAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HDTHANHTOAN()
        {
            this.CHITIETDATPHONGs = new HashSet<CHITIETDATPHONG>();
            this.CHITIETSUDUNGDVs = new HashSet<CHITIETSUDUNGDV>();
        }
    
        public int MAHD { get; set; }
        public Nullable<int> MAKHACHHANG { get; set; }
        public Nullable<decimal> TIENKHACHDUA { get; set; }
        public Nullable<decimal> TONGTIENPHONG { get; set; }
        public Nullable<decimal> TONGTIENDICHVU { get; set; }
        public Nullable<int> MANV { get; set; }
        public Nullable<System.DateTime> NGAYHD { get; set; }
        public Nullable<int> TRANGTHAI { get; set; }
        public Nullable<System.DateTime> NGAYTHANHTOAN { get; set; }
        public string PHONGDATHUE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDATPHONG> CHITIETDATPHONGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETSUDUNGDV> CHITIETSUDUNGDVs { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}