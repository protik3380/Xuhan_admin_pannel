using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class ProductUnit
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("productId")]
        public long ProductId { get; set; }

        [JsonProperty("stockKeepingUnit")]
        public string StockKeepingUnit { get; set; }

        [JsonProperty("availableStock")]
        public int AvailableStock { get; set; }

        [JsonProperty("cartonSize")]
        public int CartonSize { get; set; }

        [JsonProperty("cartonSizeUnit")]
        public string CartonSizeUnit { get; set; }

        [JsonProperty("distributorPricePerCarton")]
        public decimal DistributorPricePerCarton { get; set; }

        [JsonProperty("tradePricePerCarton")]
        public decimal TradePricePerCarton { get; set; }

        [JsonProperty("maxRetailsPrice")]
        public decimal MaxRetailsPrice { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("barcode")]
        public long Barcode { get; set; }

        [JsonProperty("barcodeView")]
        public string BarcodeView { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("productImage")]
        public List<ProductImage> ProductImages { get; set; }
    }
}