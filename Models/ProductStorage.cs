using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class ProductStorage
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("productUnitId")]
        public long ProductUnitId { get; set; }

        [JsonProperty("totalStock")]
        public int TotalStock { get; set; }

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

        [JsonProperty("unit")]
        public ProductUnit Unit { get; set; }

        public decimal WebsiteStock => Unit.AvailableStock;
    }
}