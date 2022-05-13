using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class UseProcessImageVM
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("productUnitId")]
        public long ProductUnitId { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        public HttpPostedFileBase ImageLocation { get; set; }

        //[JsonProperty("created_at")]
        //public object CreatedAt { get; set; }

        //[JsonProperty("updated_at")]
        //public object UpdatedAt { get; set; }

        //[JsonProperty("createdAt")]
        //public object CreatedAt { get; set; }

        //[JsonProperty("updatedAt")]
        //public object UpdatedAt { get; set; }
    }
}