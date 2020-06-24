using System;
using System.Collections;
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
    public partial class fFace : DevComponents.DotNetBar.OfficeForm
    {
        QLKHACHSANEntities db = new QLKHACHSANEntities();
        public fFace()
        {
            InitializeComponent();
            loadcbb();
        }
       
        void loadcbb()
        {

            comboBoxEx1.DataSource = db.GIAODIENs.Select(p => p).ToList();
            comboBoxEx1.DisplayMember = "KIEU";
            //comboBoxEx1.ValueMember = "GIATRI";
        }
        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.Text!="")
            {


                int? value =(comboBoxEx1.SelectedItem as GIAODIEN).GIATRI;
                if (value == 0)
                    this.styleManager1.ManagerStyle = eStyle.Office2007Blue;
                else if (value == 1)
                    this.styleManager1.ManagerStyle = eStyle.Office2007Silver;
                else if (value == 2)
                    this.styleManager1.ManagerStyle = eStyle.Office2007Black;
                else if (value == 3)
                    this.styleManager1.ManagerStyle = eStyle.Office2007VistaGlass;
                else if (value == 4)
                    this.styleManager1.ManagerStyle = eStyle.Office2010Silver;
                else if (value == 5)
                    this.styleManager1.ManagerStyle = eStyle.Office2010Blue;
                else if (value == 6)
                    this.styleManager1.ManagerStyle = eStyle.Office2010Black;
                else if (value == 7)
                    this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
                else if (value == 8)
                    this.styleManager1.ManagerStyle = eStyle.VisualStudio2010Blue;
                else if (value == 9)
                    this.styleManager1.ManagerStyle = eStyle.Metro;
                else if (value == 10)
                    this.styleManager1.ManagerStyle = eStyle.Office2013;
                else if (value == 11)
                    this.styleManager1.ManagerStyle = eStyle.VisualStudio2012Light;
                else if (value == 12)
                    this.styleManager1.ManagerStyle = eStyle.VisualStudio2012Dark;
                else if (value == 13)
                    this.styleManager1.ManagerStyle = eStyle.OfficeMobile2014;
                else if (value == 14)
                    this.styleManager1.ManagerStyle = eStyle.Office2016;
            }
            
        }

        private void colorPickerButton1_SelectedColorChanged(object sender, EventArgs e)
        {
            this.styleManager1.ManagerColorTint = colorPickerButton1.SelectedColor;
        }
    }
}