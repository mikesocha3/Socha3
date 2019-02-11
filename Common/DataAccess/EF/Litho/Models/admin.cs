using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    [Table("admin")]
    public class admin
    {
        public admin(int id, string type, string name, string value)
        {
            admin_id = id;
            this.type = type;
            this.name = name;
            this.value = value;
        }

        public admin(string type, string name, string value)
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }

        public admin()
        { }

        [Key]
        public int admin_id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}