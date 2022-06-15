using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Personal_PBL.DTO
{
    public class Food
    {
        private int id;
        private string displayName;
        private int idCategory;
        private float price;
        private int number;

        public int Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }
        public float Price { get => price; set => price = value; }
        public int Number { get => number; set => number = value; }

        public Food(int id,string displayName,int idCategory,float price,int number)
        {
            this.id = id;
            this.displayName = displayName;
            this.idCategory = idCategory;
            this.price = price;
            this.number = number;
        }

        public Food(DataRow row)
        {
            this.id =(int) row["id"];
            this.displayName = row["displayName"].ToString();
            this.idCategory =(int) row["idCategory"];
            this.price = float.Parse(row["price"].ToString());
            this.number = (int)row["number"];
        }
    }
}
