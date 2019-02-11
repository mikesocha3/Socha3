using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    [Table("referral")]
    public class referral
    {
        public referral()
        { }

        [Key]
        public int referral_id { get; set; }
        public string name { get; set; }
        public string timestamp { get; set; }
    }
}