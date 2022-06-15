using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personal_PBL.DTO;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace Personal_PBL.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance 
        { get { if(instance == null) instance = new FoodDAO(); return instance; }
          private set { instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetListFoodbyIdCategory(int idCategory)
        {
            List<Food> list = new List<Food>();
            DataTable table = DataProvider.Instance.ExecuteQuery($"select id,displayName,idCategory,price,number from Food where exist = 1 and number > 0 and idCategory = {idCategory}");
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Food(row));
            }
            return list;
        }

        public List<Food> GetListFoodbyName(string nameCategory)
        {
            List<Food> list = new List<Food>();
            DataTable table = DataProvider.Instance.ExecuteQuery($"select id,displayName,idCategory,price,number from Food where exist = 1 and number > 0 and displayName = N'{nameCategory}'");
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Food(row));
            }
            return list;
        }
        public DataTable GetListFood()
        {
            return DataProvider.Instance.ExecuteQuery("select Food.id,Food.displayName as 'Tên món',FoodCategory.displayName as 'Loại món', price as 'Đơn giá', number as 'số lượng' from Food inner join FoodCategory on Food.idCategory = FoodCategory.id where exist = '1'");
        }
        public void InsertFood(string name, int idCategory,float price,int number,Image image)
        {
            //nhớ thêm trường exist
            try
            {
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(image, typeof(byte[]));

                DataProvider.Instance.ExecuteNonQuery("insert into Food (displayName,idCategory,price,number,image,exist) values ( @displayName , @idCategory , @price , @number , @image , @exist )",new object[] {name,idCategory,price,number,arr,1});
                MessageBox.Show("Thêm món ăn thành công");
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public byte[] GetImageByID(int idFood)
        {
            byte[] arr = null;
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery($"select image from Food where id = {idFood}");
                DataRow dr = dt.Rows[0];
                string test = dr[0].ToString();
                if (string.IsNullOrEmpty(test)) return null;
                arr = (byte[])dr[0];
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return arr;
        }
        public void DeleteFood(int idFood)
        {
            DataProvider.Instance.ExecuteNonQuery($"update Food set exist = 0 where id = {idFood}");
        }
    }
}
