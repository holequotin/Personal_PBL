using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Personal_PBL.DTO
{
    public class BillInfor
    {
        private int id;
        private int idBill;
        private int idFood;
        private int foodNum;
        public BillInfor() { }
        public BillInfor(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdBill = (int)row["idBill"];
            this.IdFood = (int)row["idFood"];
            this.FoodNum = (int)row["foodNum"];
        }
        public int Id { get => id; set => id = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int FoodNum { get => foodNum; set => foodNum = value; }
    }
}
