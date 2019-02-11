using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    [Table("zip")]
    public class zip
    {
        public zip(int id, int code, double tax, string taxType, string region)
        {
            zip_id = id;
            this.code = code;
            this.tax = tax;
            tax_type = taxType;
            this.region = region;
        }

        public zip()
        { }

        [Key]
        public int zip_id { get; set; }
        public int code { get; set; }
        public double tax { get; set; }
        public string tax_type { get; set; }
        public string region { get; set; }
    }
}