using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Personal_PBL.DTO
{
    public class Account
    {
        private string userName;
        private string password;
        private string displayName;
        private string phoneNum;
        private string address;
        private string sex;
        private string status;
        private float salary;

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Address { get => address; set => address = value; }
        public string Status { get => status; set => status = value; }
        public float Salary { get => salary; set => salary = value; }
        public string Sex { get => sex; set => sex = value; }

        public Account()
        {

        }
        public Account(string userName,string password,string displayName,string sex,string phoneNum,string address,string status,float salary)
        {
            this.UserName = userName;
            this.Password = password;
            this.DisplayName = displayName;
            this.PhoneNum = phoneNum;
            this.Address = address;
            this.Status = status;
            this.Salary = salary;
            this.Sex = sex;
        }
        public Account(DataRow row)
        {
            if (row == null) new Account();
            else
            {
                this.UserName = (string)row["userName"];
                this.Password = (string)row["passWord"];
                this.DisplayName = (string)row["displayName"];
                this.PhoneNum = (string)row["phoneNum"];
                this.Address = (string)row["address"];
                this.Status = (string)row["status"];
                this.Salary = float.Parse(row["salary"].ToString());
                this.Sex = (string)row["sex"];
            }
        }
    }
}
