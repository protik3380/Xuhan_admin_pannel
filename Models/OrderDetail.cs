using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class OrderDetail
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("productUnitId")]
        public long ProductUnitId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("priceAfterDiscount")]
        public decimal PriceAfterDiscount { get; set; }

        [JsonProperty("packetQuantity")]
        public int PacketQuantity { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("productUnit")]
        public ProductUnit ProductUnit { get; set; }
    }
}