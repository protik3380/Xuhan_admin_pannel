using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class Category
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "Please enter category name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("createdAt")]
        public Nullable<DateTime> CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public Nullable<DateTime> UpdatedAt { get; set; }

        [Required(ErrorMessage = "Please choose category image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}