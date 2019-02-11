using System;
using System.ComponentModel.DataAnnotations;

namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    public class nutrition_nutrient
    {
        [Key]
        public int nutrition_nutrient_id { get; set; }
        public string added_item { get; set; }
        public string is_or_contains { get; set; }
        public string name { get; set; }
        public string nutrient_master_id { get; set; }
        public string old_value_prepared_type { get; set; }
        public string percent { get; set; }
        public string quantity { get; set; }
        public string serving_size { get; set; }
        public string serving_size_uom { get; set; }
        public string servings_per_container { get; set; }
        public string type { get; set; }
        public string uom { get; set; }
        public string upc { get; set; }
        public string value_prepared_type { get; set; }
    }
}