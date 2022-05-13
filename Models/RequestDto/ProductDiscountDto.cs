using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class ProductDiscountDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("productUnitId")]
        public int ProductUnitId { get; set; }

        [JsonProperty("discountPercentage")]
        public int DiscountPercentage { get; set; }

        [JsonProperty("activateDate")]
        public DateTime ActivateDate { get; set; }

        [JsonProperty("endValidityDate")]
        public DateTime EndValidityDate { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("modifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}