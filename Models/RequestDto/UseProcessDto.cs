using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class UseProcessDto
    {
        public long ProductUnitId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }

        [JsonProperty("useProcessImages")]
        public List<UseProcessImageDto> UseProcessImages { get; set; }
        public List<UseProcessImageDto> UseProcessImagesForm { get; set; }
    }
}