using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Personal_PBL.DAO;

namespace Personal_PBL
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            drvFood.DataSource = foodList;
            LoadListBill();
            LoadListFood();
            LoadListCategory();
            AddBindingFood();
        }
        private void LoadListBill()
        {
            drvListBill.DataSource = BillDAO.Instance.GetListBillByDate(dtpkDataFrom.Value, dtpkDateTo.Value);     
        }
        private void LoadListCategory()
        {
            cbbFoodCategory.DataSource = CategoryDAO.Instance.GetListCategory();
            cbbFoodCategory.DisplayMember = "displayName";
        }
        private void btnShowBill_Click(object sender, EventArgs e)
        {
            LoadListBill();
        }
        private void AddBindingFood()
        {
            txtFoodID.DataBindings.Add(new Binding("Text", drvFood.DataSource, "id"));
            txtFoodName.DataBindings.Add(new Binding("Text", drvFood.DataSource, "Tên món"));
            nmrFoodPrice.DataBindings.Add(new Binding("Value", drvFood.DataSource, "Đơn giá"));
            nmrFoodNumber.DataBindings.Add(new Binding("Value", drvFood.DataSource, "Số lượng"));
            cbbFoodCategory.DataBindings.Add(new Binding("Text", drvFood.DataSource, "Loại món"));
        }
        private void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        private void InsertFood()
        {
            //insert here
        }
        private void btnFoodShow_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void btnFoodAdd_Click(object sender, EventArgs e)
        {
            AddFood f = new AddFood();
            f.ShowDialog();
            LoadListFood();
        }

        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            int idFood = Convert.ToInt32(txtFoodID.Text);
            byte[] arr = FoodDAO.Instance.GetImageByID(idFood);
            if (arr == null)
            {
                picFoodImage.Image = null;
            }
            else
            {
                MemoryStream mstream  = new MemoryStream(arr);
                picFoodImage.Image = Image.FromStream(mstream);
            }
        }

        private void btnFoodDelete_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int idFood = Convert.ToInt32(txtFoodID.Text.Trim());
            if (MessageBox.Show($"Bạn có chắc chắn xóa món {name} ??","Cảnh báo",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                FoodDAO.Instance.DeleteFood(idFood);
                LoadListFood();
            }
        }

        private void btnFoodUpdate_Click(object sender, EventArgs e)
        {
            if (btnFoodUpdate.Text.Equals("Sửa"))
            {
                btnFoodUpdate.Text = "Lưu";

                txtFoodName.ReadOnly = false;
                cbbFoodCategory.Enabled = true;
                nmrFoodPrice.ReadOnly = false;
                nmrFoodNumber.ReadOnly = false;
                btnBrowse.Visible = true;
            }
            else
            {
                btnFoodUpdate.Text = "Sửa";
                //Lưu thông tin mới ở đây
                txtFoodName.ReadOnly = true;
                cbbFoodCategory.Enabled = false;
                nmrFoodPrice.ReadOnly = true;
                nmrFoodNumber.ReadOnly = true;
                btnBrowse.Visible = false;
            }
        }

    }
}
