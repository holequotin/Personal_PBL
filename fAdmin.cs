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
using Personal_PBL.DTO;

namespace Personal_PBL
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        private int selected_id = 0;
        private string name;
        private float price;
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
            drvFood.Columns["id"].Visible = false;
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
        private void LoadImage()
        {
            int idFood = selected_id;
            byte[] arr = FoodDAO.Instance.GetImageByID(idFood);
            if (arr == null)
            {
                picFoodImage.Image = null;
            }
            else
            {
                MemoryStream mstream = new MemoryStream(arr);
                picFoodImage.Image = Image.FromStream(mstream);
            }
        }
        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            selected_id = Convert.ToInt32(txtFoodID.Text);
            LoadImage();
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

                name = txtFoodName.Text;
                price = (float)nmrFoodPrice.Value;
            }
            else
            {
                btnFoodUpdate.Text = "Sửa";
                if (txtFoodName.Text.Trim().Equals(name) && nmrFoodPrice.Value.ToString().Equals(price.ToString()))
                {
                    MessageBox.Show("Chỉ update");
                    Category category = cbbFoodCategory.SelectedItem as Category;
                    FoodDAO.Instance.UpdateFood(selected_id, category.Id, (int)nmrFoodNumber.Value, picFoodImage.Image);
                }
                else
                {
                    MessageBox.Show("Xóa và thêm món tương tự");
                    FoodDAO.Instance.DeleteFood(selected_id);
                    Category category = cbbFoodCategory.SelectedItem as Category;
                    FoodDAO.Instance.InsertFood(txtFoodName.Text.Trim(), category.Id, (float)nmrFoodPrice.Value, (int)nmrFoodNumber.Value, picFoodImage.Image);
                }
                txtFoodName.ReadOnly = true;
                cbbFoodCategory.Enabled = false;
                nmrFoodPrice.ReadOnly = true;
                nmrFoodNumber.ReadOnly = true;
                btnBrowse.Visible = false;
                LoadListFood();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picFoodImage.Image = Image.FromFile(ofd.FileName);//add file from fileDialog
                }
            }
            //note
        }
    }
}
