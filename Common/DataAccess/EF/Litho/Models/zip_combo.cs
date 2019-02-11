using System;
using System.ComponentModel.DataAnnotations;

namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    public class zip_combo
    {
        public zip_combo(int id, int code, double tax)
        {
            zip_combo_id = id;
            this.code = code;
            this.tax = tax;
        }

        public zip_combo()
        { }

        [Key]
        public int zip_combo_id { get; set; }
        public int code { get; set; }
        public double tax { get; set; }
    }
}