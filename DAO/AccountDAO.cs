using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Personal_PBL.DTO;

namespace Personal_PBL.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = new AccountDAO();

        public static AccountDAO Instance { get { if (instance == null) instance = new AccountDAO(); return instance; } private set { instance = value; } }//singleton
        private AccountDAO() { }

        public bool Login(string userName,string passWord)
        {
            string query = " USP_Login @userName , @passWord ";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query,new object[] {userName,passWord});

            return dt.Rows.Count > 0;
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery($"select * from Account where username = '{userName}'");
            foreach (DataRow dr in dt.Rows)
            {
                return new Account(dr);
            }
            return null;
        }
    }

}
