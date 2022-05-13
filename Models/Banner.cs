using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class Banner
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("page")]
        [Required(ErrorMessage = "Please enter banner page name")]
        public string Page { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Please choose banner image")]
        public HttpPostedFileBase Image { get; set; }
    }
}