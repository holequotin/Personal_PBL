using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Personal_PBL.DTO
{
    public class Category
    {
        private int id;
        private string displayName;

        public int Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }

        public Category(int id,string displayName)
        {
            this.Id = id;
            this.DisplayName = displayName;
        }
        public Category(DataRow row)
        {
            this.Id=(int)row["id"];
            this.DisplayName = row["displayname"].ToString();
        }
    }
}
