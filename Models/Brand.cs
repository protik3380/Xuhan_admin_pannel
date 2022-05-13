using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class Brand
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "Please enter brand name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

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

        [Required(ErrorMessage = "Please choose brand image")]

        public HttpPostedFileBase BrandImage { get; set; }


    }
}