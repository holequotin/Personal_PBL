using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personal_PBL.DTO;
using System.Data;

namespace Personal_PBL.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        private MenuDAO() { }

        public static MenuDAO Instance 
        {   get { if (instance == null) instance = new MenuDAO(); return instance; }
            private set { instance = value; }
        }

        public List<Menu> GetListByTable(int idTable)
        {
            List<Menu> list = new List<Menu>();
            string query = "SELECT Food.displayname , Food.price , BillInfo.foodNum , BillInfo.foodNum * Food.price as TotalPrice FROM Bill INNER JOIN BillInfo ON Bill.id = BillInfo.idBill INNER JOIN Food ON BillInfo.idFood = Food.id WHERE idTable = @idTable AND Bill.status = N'Chưa thanh toán'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query,new object[] {idTable.ToString()});
            foreach (DataRow dr in dt.Rows)
            {
                Menu menu = new Menu(dr);
                list.Add(menu);
            }
            return list;
        }
    }
}
