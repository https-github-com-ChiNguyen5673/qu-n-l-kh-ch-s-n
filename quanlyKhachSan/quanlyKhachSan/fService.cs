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
    public partial class fService : DevComponents.DotNetBar.OfficeForm
    {
        QLKHACHSANEntities db = new QLKHACHSANEntities();
       

        
        BindingSource bindingService = new BindingSource();
        public fService()
        {
            InitializeComponent();
            dtgvService.DataSource = bindingService;
            loadListService();
            AddServiceBinding();
            loadCbBGroupService();
        }

        private void loadCbBGroupService()
        {
            cbbGroupService.DataSource = db.LOAIDICHVUs.Select(p => p).ToList();
            cbbGroupService.DisplayMember = "TENLOAIDICHVU";
            cbbGroupService.ValueMember = "MALOAIDICHVU";
        }

        private void AddServiceBinding()
        {
            txtServiceId.DataBindings.Add(new Binding("Text", dtgvService.DataSource, "MADICHVU", true, DataSourceUpdateMode.Never));
            txtServiceName.DataBindings.Add(new Binding("Text", dtgvService.DataSource, "TENDICHVU", true, DataSourceUpdateMode.Never));
            nmServicePrice.DataBindings.Add(new Binding("Value", dtgvService.DataSource, "GIADV", true, DataSourceUpdateMode.Never));
            nmServiceAmount.DataBindings.Add(new Binding("Value", dtgvService.DataSource, "SOLUONGTON", true, DataSourceUpdateMode.Never));
            txtServiceUnit.DataBindings.Add(new Binding("Text", dtgvService.DataSource, "DONVI", true, DataSourceUpdateMode.Never));
        }

        private void loadListService()
        {
            bindingService.DataSource = db.USP_ViewService();
        }

        private void txtServiceId_TextChanged(object sender, EventArgs e)
        {
            if (dtgvService.SelectedCells.Count > 0 && dtgvService.SelectedCells[0].OwningRow.Cells["clGroupServiceID"].Value != null)
            {
                int idG = Convert.ToInt16(dtgvService.SelectedCells[0].OwningRow.Cells["clGroupServiceID"].Value.ToString());

                LOAIDICHVU groupService = db.LOAIDICHVUs.Where(p => p.MALOAIDICHVU == idG).SingleOrDefault(); ;


                cbbGroupService.SelectedItem = groupService;

                int indexG = -1;
                int iG = 0;
                foreach (LOAIDICHVU item in cbbGroupService.Items)
                {
                    if (item.MALOAIDICHVU == groupService.MALOAIDICHVU)
                    {
                        indexG = iG;
                        break;
                    }
                    iG++;
                }
                cbbGroupService.SelectedIndex = indexG;
            }
        }

        private void btnServiceView_Click(object sender, EventArgs e)
        {
            loadListService();
        }

        private void btnServiceAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string serviceName = txtServiceName.Text;               
                int? groupServiceID = Convert.ToInt16(cbbGroupService.SelectedValue);
                decimal servicePrice = nmServicePrice.Value;
                int serviceAmount = nmServiceAmount.Value;
                string unit = txtServiceUnit.Text;
                db.USP_InsertService(serviceName, servicePrice,"", serviceAmount, unit, groupServiceID);
                loadListService();
                MessageBox.Show("Thêm phòng thành công !");
               
            }
            catch
            {
                MessageBox.Show("có lỗi khi thêm ! vui lòng kiểm tra giá trị nhập vào");
            }
        }

        private void btnServiceEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt16(txtServiceId.Text);
                string serviceName = txtServiceName.Text;
                int? groupServiceID = Convert.ToInt16(cbbGroupService.SelectedValue);
                decimal servicePrice = nmServicePrice.Value;
                int serviceAmount = nmServiceAmount.Value;
                string unit = txtServiceUnit.Text;
                db.USP_UpdateService(id,serviceName, servicePrice, "", serviceAmount, unit, groupServiceID);
                loadListService();
                MessageBox.Show("Sửa phòng thành công !");

            }
            catch
            {
                MessageBox.Show("có lỗi khi sửa ! vui lòng kiểm tra giá trị nhập vào");
            }
        }

        private void btnServiceRemove_Click(object sender, EventArgs e)
        {
            int serviceId = Convert.ToInt16(txtServiceId.Text);
            if (MessageBox.Show("Bạn có chắc muốn xóa phòng :" + txtServiceName.Text + " ?\nViệc này có thể sẽ dẫn đến mất dữ liệu về thông tin hóa đơn  ", "Thông báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                if (db.USP_DeleteService(serviceId) == 1)
                {
                    MessageBox.Show("Xóa sản phẩm thành công !");
                    loadListService();
                    

                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa !");
                }
            }
        }

        private void txtServiceSearch_TextChanged(object sender, EventArgs e)
        {
            bindingService.DataSource = db.USP_SearchService(txtServiceSearch.Text);
        }
    }
}