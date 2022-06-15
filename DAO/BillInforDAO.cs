using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Personal_PBL.DTO;

namespace Personal_PBL.DAO
{
    public class BillInforDAO
    {
        private static BillInforDAO instance;

        private BillInforDAO() { }

        public static BillInforDAO Instance 
        {
            get { if (instance == null) instance = new BillInforDAO(); return instance; }
            private set { instance = value; }
        }
        public List<BillInfor> GetListBillInfor(int idBill)
        {
            List<BillInfor> listBillInfors = new List<BillInfor>();
            string query = $"select * from BillInfo where idBill = {idBill}";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            for(int i = 0;i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                listBillInfors.Add(new BillInfor(row));
            }
            return listBillInfors;
        }
        public void InsertBillInfor(int idBill,int idFood,int foodNum)
        {
            string query = "EXECUTE USP_InsertBillInfo @idBill , @idFood , @foodNum";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill, idFood, foodNum });
        }
    }
}
