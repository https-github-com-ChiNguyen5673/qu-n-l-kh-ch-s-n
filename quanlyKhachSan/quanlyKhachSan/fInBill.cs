using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using quanlyKhachSan.DAO;
using quanlyKhachSan.rpCrystalReport;

namespace quanlyKhachSan
{
    public partial class fInBill : DevComponents.DotNetBar.OfficeForm
    {
        private int idBill;
        public int IdBill { get => idBill; set => idBill = value; }
        QLKHACHSANEntities db = new QLKHACHSANEntities();
        public fInBill(int idbill, DataSet Bill)
        {
            InitializeComponent();
            IdBill = idbill;
            loadRp(IdBill, Bill);
        }

        

        void loadRp(int idBill,DataSet dsBill)
        {
            
            
            crBill cR = new crBill();
            cR.Database.Tables["Bill"].SetDataSource(dsBill.Tables[0]);
            cR.Database.Tables["DetailBillRoom"].SetDataSource(dsBill.Tables[1]);
            cR.Database.Tables["DetailBillService"].SetDataSource(dsBill.Tables[2]);

            crvBill.ReportSource = null;
            crvBill.ReportSource = cR;
        }
    }
}