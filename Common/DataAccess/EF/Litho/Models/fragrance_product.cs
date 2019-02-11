using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    [Table("fragrance_product")]
    public class fragrance_product
    {
        [Key]
        public int fragrance_product_id { get; set; }
        public string product_id { get; set; }
        public string sku { get; set; }
        public string image_url { get; set; }
        public string thumbnail_url { get; set; }
        public string name { get; set; }
        public string variation { get; set; }
        public string title { get; set; }
        public string quantity { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string short_description { get; set; }
        public string sample_retail { get; set; }
        public string cost { get; set; }
        public string manufacturer { get; set; }
        public string manufacturer_id { get; set; }
        public string category { get; set; }
        public string brand { get; set; }
        public string weight { get; set; }
        public string price { get; set; }
        public string map_price { get; set; }
        public string msrp { get; set; }
        public string upc { get; set; }
        public string asin { get; set; }
        public string custom_shipping_rate { get; set; }
    }
}