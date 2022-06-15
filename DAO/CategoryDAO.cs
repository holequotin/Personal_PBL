using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Personal_PBL.DTO;

namespace Personal_PBL.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        private CategoryDAO() { }

        public static CategoryDAO Instance
        { 
            get { if (instance == null) instance = new CategoryDAO(); return instance; }
            private set { instance = value; }
        }
        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            DataTable tableCate = DataProvider.Instance.ExecuteQuery("select * from FoodCategory");
            
            for(int i = 0;i < tableCate.Rows.Count; i++)
            {
                list.Add(new Category(tableCate.Rows[i]));
            }
           
            return list;
        }
    }
}
