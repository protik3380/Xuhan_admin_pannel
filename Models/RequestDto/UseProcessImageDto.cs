using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class UseProcessImageDto
    {
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("productUnitId")]
        public long ProductUnitId { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        public HttpPostedFileBase ImageLocation { get; set; }
    }
}