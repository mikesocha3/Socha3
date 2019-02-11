using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Socha3.Common.DataAccess.EF.Domo.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieLink { get; set; }
    }
}