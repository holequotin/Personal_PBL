using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personal_PBL.DTO;
using System.Data;

namespace Personal_PBL.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public static TableDAO Instance { get { if (instance == null) instance = new TableDAO(); return instance; } private set { instance = value; } }

        private TableDAO() { }
        public  List<Table> LoadTableList()
        {
            List<Table> list = new List<Table>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("execute USP_GetTableList");
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(new Table(dr));
            }
            return list; 
        }
        public void CheckIn(int idTable)
        {
            string query = $"update TableFood set status = N'Có người' where id = {idTable}";
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public void CheckOut(int idTable)
        {
            string query = $"update TableFood set status = N'Còn trống' where id = {idTable}";
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public List<Table> LoadTableListExceptIDTable(int idTable)
        {
            List<Table> list = new List<Table>();
            string query = $"select * from TableFood where id != {idTable}";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow dr in dt.Rows)
            {
                list.Add(new Table(dr));
            }
            return list;
        } 
        public void SwapTable(int idTableFrom,int idTableTo)
        {
            int idBillFrom = BillDAO.Instance.GetUnCheckBillIDByTableID(idTableFrom);
            int idBillTo = BillDAO.Instance.GetUnCheckBillIDByTableID(idTableTo);
            if(idBillTo == -1)
            {
                DataProvider.Instance.ExecuteNonQuery($"update Bill set idTable = {idTableTo} where id = {idBillFrom}");
                TableDAO.Instance.CheckOut(idTableFrom);
                TableDAO.Instance.CheckIn(idTableTo);
            }
            else
            {
                DataProvider.Instance.ExecuteNonQuery($"update Bill set idTable = {idTableTo} where id = {idBillFrom}");
                DataProvider.Instance.ExecuteNonQuery($"update Bill set idTable = {idTableFrom} where id = {idBillTo}");
            }
        }
    }
}
