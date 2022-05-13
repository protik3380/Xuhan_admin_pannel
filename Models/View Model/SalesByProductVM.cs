using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class SalesByProductVM
    {
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("productUnit")]
        public string ProductUnit { get; set; }

        [JsonProperty("totalProducts")]
        public string TotalProducts { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("totalOrders")]
        public int TotalOrders { get; set; }

        //[JsonProperty("productTypeName")]
        //public string ProductTypeName { get; set; }

        [JsonProperty("brandName")]
        public string BrandName { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        
        [JsonProperty("priceAfterDiscount")]
        public decimal PriceAfterDiscount { get; set; }

        public decimal TotalPrice => Convert.ToDecimal(TotalProducts) * PriceAfterDiscount;
    }
}