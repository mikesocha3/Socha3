using System;
using System.ComponentModel.DataAnnotations;

namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    public class nutrition_product
    {
        [Key]
        public int nutrition_product_id { get; set; }
        public string stock_code { get; set; }
        public string upc { get; set; }
        public string brand { get; set; }
        public string brand_site { get; set; }
        public string brand_logo { get; set; }
        public string super_category { get; set; }
        public string sub_category { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string wholesale { get; set; }
        /// <summary>
        /// calculated
        /// </summary>
        public string cost { get; set; }
        /// <summary>
        /// calculated
        /// </summary>
        public string price { get; set; }
        public string map_price { get; set; }
        public string retail { get; set; }
        public string pounds { get; set; }
        public string size { get; set; }
        public string flavor { get; set; }
        public string description { get; set; }
        public string short_description { get; set; }
        public string directions { get; set; }
        public string warnings { get; set; }
        public string ingredients { get; set; }
        public string clt_stock { get; set; }
        public string fre_stock { get; set; }
        public string mes_stock { get; set; }
        public string str_stock { get; set; }
        public string wnd_stock { get; set; }
        public string orl_stock { get; set; }
        public string discontinued { get; set; }
        public string list_date { get; set; }
        public string post_date { get; set; }
        public string image { get; set; }
        public string thumbnail { get; set; }
    }
}