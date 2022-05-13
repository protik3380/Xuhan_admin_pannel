using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class ProductStorageDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("productUnitId")]
        public int ProductUnitId { get; set; }

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
    }
}