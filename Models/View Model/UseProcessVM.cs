using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class UseProcessVM
    {
        [JsonProperty("productUnitId")]
        public long ProductUnitId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("stockKeepingUnit")]
        public string StockKeepingUnit { get; set; }

        [JsonProperty("productId")]
        public long ProductId { get; set; }

        [JsonProperty("useProcessImage")]
        public List<UseProcessImageVM> UseProcessImage { get; set; }

        [JsonProperty("useProcessImages")]
        public List<UseProcessImageVM> UseProcessImages { get; set; }

        public List<UseProcessImageVM> UseProcessImagesForm { get; set; }
    }
}