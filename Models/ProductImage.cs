using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class ProductImage
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("productUnitId")]
        public long ProductUnitId { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        //[JsonProperty("isActive")]
        //public bool IsActive { get; set; }

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

    }
}