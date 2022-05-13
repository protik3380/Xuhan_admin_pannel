using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class ProductUnitDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("productId")]
        [Required(ErrorMessage = "Please select product")]
        public int ProductId { get; set; }

        [JsonProperty("stockKeepingUnit")]
        [Required(ErrorMessage = "Please enter stock keeping unit")]
        public string StockKeepingUnit { get; set; }

        [JsonProperty("availableStock")]
        public int AvailableStock { get; set; }

        [JsonProperty("cartonSize")]
        [Required(ErrorMessage = "Please enter carton size")]
        public int CartonSize { get; set; }

        [JsonProperty("cartonSizeUnit")]
        [Required(ErrorMessage = "Please enter carton size unit")]
        public string CartonSizeUnit { get; set; }

        [JsonProperty("distributorPricePerCarton")]
        [Required(ErrorMessage = "Please enter distributor price per carton")]
        public decimal DistributorPricePerCarton { get; set; }

        [JsonProperty("tradePricePerCarton")]
        [Required(ErrorMessage = "Please enter trade price per carton")]
        public decimal TradePricePerCarton { get; set; }

        [JsonProperty("maxRetailsPrice")]
        [Required(ErrorMessage = "Please enter maximum retail price")]
        public decimal MaximumRetailPrice { get; set; }

        [JsonProperty("effectiveDate")]
        [Required(ErrorMessage = "Please select effective date")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("barcode")]
        public long Barcode { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("productImage")]
        public List<ProductImageDto> ProductImages { get; set; }

        public List<ProductImageDto> ProductImagesForm { get; set; }
        public string ProductName { get; set; }
    }
}