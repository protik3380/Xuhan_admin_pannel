using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;


namespace xtbdEcommerceAdminPanel.Models
{
    public class Coupon
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        [Required(ErrorMessage = "Please enter coupon code")]
        public string Code { get; set; }

        [JsonProperty("discountPercentage")] 
        public decimal? DiscountPercentage { get; set; }

        [JsonProperty("discountAmount")]
        [Required(ErrorMessage = "Please enter discount amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal DiscountAmount { get; set; }

        [JsonProperty("minimumOrderAmount")]
        [Required(ErrorMessage = "Please enter minimum order amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal MinimumOrderAmount { get; set; }

        [Required(ErrorMessage = "Please enter maximum order amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]

        [JsonProperty("maximumOrderAmount")]
        public decimal MaximumOrderAmount { get; set; }

        [JsonProperty("validity")]
        [Required(ErrorMessage = "Please enter validity date")]
        public DateTime Validity { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}