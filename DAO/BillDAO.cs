using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Personal_PBL.DTO;
using System.Windows.Forms;

namespace Personal_PBL.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance 
        { 
            get { if (instance == null) instance = new BillDAO();return instance;}
            private set { instance = value; }
        }
        private BillDAO() { }

        //Trả về  BillID nếu bàn có Bill chưa checkout
        //-1 nếu không có
        public int GetUnCheckBillIDByTableID(int tableID)
        {
            string query = $"select * from Bill where idTable = {tableID} and status = N'Chưa thanh toán'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Bill newBill = new Bill(dr);
                return newBill.Id;
            }
            return -1; 
        }
        public void InsertBill(int idTable)
        {
            try
            {
                string query = "EXECUTE USP_InsertBill @idTable";
                DataProvider.Instance.ExecuteNonQuery(query, new object[] { idTable });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().ToString() + ":" + ex.Message);
            }
        }
        public int GetMaxIdBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM Bill");
            }catch (Exception ex)
            {
                return 1;
            }
        }
        public void CheckOut(int idBill,int discount,float totalPrice)
        {
            string query = $"update Bill set datecheckout = getdate() , status = N'Đã thanh toán' , discount = {discount} , totalPrice = {totalPrice} where id = {idBill}";
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public DataTable GetListBillByDate(DateTime checkIn,DateTime checkOut)
        {
            return DataProvider.Instance.ExecuteQuery("EXECUTE USP_GetListBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }
    }
}
