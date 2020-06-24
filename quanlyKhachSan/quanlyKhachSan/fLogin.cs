using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace quanlyKhachSan
{
    public partial class fLogin : DevComponents.DotNetBar.OfficeForm
    {
        QLKHACHSANEntities db = new QLKHACHSANEntities();
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassWord.Text;
            TAIKHOAN user = db.TAIKHOANs.Where(p => p.TENDANGNHAP == username && p.MATKHAU == password).FirstOrDefault();
            if (user != null)
            {
                this.Hide();

                Form1 f = new Form1(user);

                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng !");
            }
        }
    }
}