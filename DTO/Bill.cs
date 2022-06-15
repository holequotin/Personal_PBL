using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Personal_PBL.DTO
{
    public class Bill
    {
        private int id;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int idTable;
        private string status;
        private float discount;

        public Bill() { }
        public Bill(int id, DateTime? dateCheckIn,DateTime? dateCheckOut,int idTable,string status,float discount = 0)
        {
            this.id = id;
            this.dateCheckIn = dateCheckIn;
            this.DateCheckOut = DateCheckOut;
            this.idTable = idTable;
            this.status = status;
            this.discount = discount;
        }
        public Bill(DataRow row)
        {
            id = Convert.ToInt32(row["id"]);
            dateCheckIn = (DateTime)row["datecheckin"];
           var dateCheckOutTemp = row["datecheckout"];
            if (dateCheckOutTemp.ToString() != "") this.DateCheckOut = (DateTime)dateCheckOutTemp;
            idTable = (int)row["idTable"];
            status = Convert.ToString(row["status"]);
            discount = float.Parse(row["discount"].ToString());
        }
        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int IdTable { get => idTable; set => idTable = value; }
        public string Status { get => status; set => status = value; }
    }
}
