using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Personal_PBL.DTO
{
    public class Table
    {
        private int id;
        private string name;
        private string status;
        public Table(int id = 0, string name = null,string status = null) {
            this.id = id;
            this.name = name;
            this.status = status;
        }
        public Table(DataRow data)
        {
            this.id = (int)data["id"];
            this.name = data["displayName"].ToString();
            this.status = data["status"].ToString();
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }
    }
}
