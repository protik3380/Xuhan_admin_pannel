using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class ProductDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "Please enter product name")]
        public string Name { get; set; }

        [JsonProperty("ingredients")]
        public string Ingredients { get; set; }

        [JsonProperty("useProcess")]
        public string UseProcess { get; set; }

        [JsonProperty("videoLink")]
        public string VideoLink { get; set; }

        [JsonProperty("categoryId")]
        [Required(ErrorMessage = "Please choose category")]
        public long? CategoryId { get; set; }

        [JsonProperty("brandId")]
        [Required(ErrorMessage = "Please choose brand")]
        public long? BrandId { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public Nullable<DateTime> CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public Nullable<DateTime> UpdatedAt { get; set; }

        [JsonProperty("concernTagId")]
        public List<long> ConcernTagId { get; set; }

        [JsonProperty("solutionTagId")]
        public List<long> SolutionTagId { get; set; }

    }
}