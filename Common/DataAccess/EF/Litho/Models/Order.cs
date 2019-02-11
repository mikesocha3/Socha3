using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    [Table("order")]
    public class order
    {
        [Key]
        public int order_id { get; set; }
        public string serial_number { get; set; }
        public string timestamp { get; set; }
        public string shipping_name { get; set; }
        public string shipping_cost { get; set; }
        public string total_tax { get; set; }
        public string adjustment_total { get; set; }
        public string item_names { get; set; }
        public string item_descriptions { get; set; }
        public string item_quantities { get; set; }
        public string item_unit_prices { get; set; }
        public string buyer_id { get; set; }
        public string google_order_number { get; set; }
        public string sandbox_or_live { get; set; }
        public string store_name { get; set; }
        public string shipping_company_name { get; set; }
        public string shipping_first_name { get; set; }
        public string shipping_last_name { get; set; }
        public string shipping_email { get; set; }
        public string shipping_phone { get; set; }
        public string shipping_fax { get; set; }
        public string shipping_address1 { get; set; }
        public string shipping_address2 { get; set; }
        public string shipping_country_code { get; set; }
        public string shipping_city { get; set; }
        public string shipping_region { get; set; }
        public string shipping_postal_code { get; set; }
        public string billing_company_name { get; set; }
        public string billing_first_name { get; set; }
        public string billing_last_name { get; set; }
        public string billing_email { get; set; }
        public string billing_phone { get; set; }
        public string billing_fax { get; set; }
        public string billing_address1 { get; set; }
        public string billing_address2 { get; set; }
        public string billing_country_code { get; set; }
        public string billing_city { get; set; }
        public string billing_region { get; set; }
        public string billing_postal_code { get; set; }
        public string email_allowed { get; set; }
        public string order_subtotal { get; set; }
        public string order_total { get; set; }
        public string fulfillment_order_state { get; set; }
        public string financial_order_state { get; set; }
        public string total_refund_amount { get; set; }
        public string total_chargeback_amount { get; set; }
        public string message { get; set; }
        public string coupon { get; set; }
        public string invoice_sent { get; set; }
        public string net_profit { get; set; }
    }
}