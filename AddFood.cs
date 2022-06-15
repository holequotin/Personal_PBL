using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Personal_PBL.DAO;
using Personal_PBL.DTO;

namespace Personal_PBL
{
    public partial class AddFood : Form
    {
        public AddFood()
        {
            InitializeComponent();
            LoadCategoryList();
        }
        private void LoadCategoryList()
        {
            cbbCategory.DataSource = CategoryDAO.Instance.GetListCategory();
            cbbCategory.DisplayMember = "displayName";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    picFoodImage.Image = Image.FromFile(ofd.FileName);//add file from fileDialog
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            int idCategory = (cbbCategory.SelectedItem as Category).Id;
            float price =(float) nmrPrice.Value;
            int number =(int) nmrNumber.Value;
            Image image = picFoodImage.Image;

            if(string.IsNullOrEmpty(name)||picFoodImage.Image == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                FoodDAO.Instance.InsertFood(name, idCategory, price, number, image);
                Close();
            }
        }
    }
}
