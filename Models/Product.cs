using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ingredients")]
        public string Ingredients { get; set; }

        [JsonProperty("useProcess")]
        public string UseProcess { get; set; }

        [JsonProperty("categoryId")]
        public long? CategoryId { get; set; }

        [JsonProperty("brandId")]
        public long? BrandId { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("videoLink")]
        public string VideoLink { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public Nullable<DateTime> CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public Nullable<DateTime> UpdatedAt { get; set; }

        [JsonProperty("brand")]
        public Brand Brand { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("concernTags")]
        public List<ConcernTag> ConcernTags { get; set; }

        [JsonProperty("solutionTags")]
        public List<SolutionTag> SolutionTags { get; set; }
    }
}