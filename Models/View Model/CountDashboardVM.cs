using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class CountDashboardVM
    {
        [JsonProperty("totalBanner")]
        public int TotalBanner { get; set; }

        [JsonProperty("activeBanner")]
        public int ActiveBanner { get; set; }

        [JsonProperty("inactiveBanner")]
        public int InactiveBanner { get; set; }

        [JsonProperty("totalBrand")]
        public int TotalBrand { get; set; }

        [JsonProperty("activeBrand")]
        public int ActiveBrand { get; set; }

        [JsonProperty("inactiveBrand")]
        public int InactiveBrand { get; set; }

        [JsonProperty("totalCategory")]
        public int TotalCategory { get; set; }

        [JsonProperty("activeCategory")]
        public int ActiveCategory { get; set; }

        [JsonProperty("inactiveCategory")]
        public int InactiveCategory { get; set; }

        [JsonProperty("totalProduct")]
        public int TotalProduct { get; set; }

        [JsonProperty("activeProduct")]
        public int ActiveProduct { get; set; }

        [JsonProperty("inactiveProduct")]
        public int InactiveProduct { get; set; }

        [JsonProperty("totalProductUnit")]
        public int TotalProductUnit { get; set; }

        [JsonProperty("activeProductUnit")]
        public int ActiveProductUnit { get; set; }

        [JsonProperty("inactiveProductUnit")]
        public int InactiveProductUnit { get; set; }

        [JsonProperty("totalSolutionTag")]
        public int TotalSolutionTag { get; set; }

        [JsonProperty("activeSolutionTag")]
        public int ActiveSolutionTag { get; set; }

        [JsonProperty("inactiveSolutionTag")]
        public int InactiveSolutionTag { get; set; }

        [JsonProperty("totalConcernTag")]
        public int TotalConcernTag { get; set; }

        [JsonProperty("activeConcernTag")]
        public int ActiveConcernTag { get; set; }

        [JsonProperty("inactiveConcernTag")]
        public int InactiveConcernTag { get; set; }

        [JsonProperty("totalDepartment")]
        public int TotalDepartment { get; set; }

        [JsonProperty("activeDepartment")]
        public int ActiveDepartment { get; set; }

        [JsonProperty("inactiveDepartment")]
        public int InactiveDepartment { get; set; }

        [JsonProperty("totalDesignation")]
        public int TotalDesignation { get; set; }

        [JsonProperty("activeDesignation")]
        public int ActiveDesignation { get; set; }

        [JsonProperty("inactiveDesignation")]
        public int InactiveDesignation { get; set; }

        [JsonProperty("totalMasterDeport")]
        public int TotalMasterDeport { get; set; }

        [JsonProperty("activeMasterDeport")]
        public int ActiveMasterDeport { get; set; }

        [JsonProperty("inactiveMasterDeport")]
        public int InactiveMasterDeport { get; set; }

        [JsonProperty("totalDistributor")]
        public int TotalDistributor { get; set; }

        [JsonProperty("activeDistributor")]
        public int ActiveDistributor { get; set; }

        [JsonProperty("inactiveDistributor")]
        public int InactiveDistributor { get; set; }

        [JsonProperty("totalDeliveryman")]
        public int TotalDeliveryman { get; set; }

        [JsonProperty("activeDeliveryman")]
        public int ActiveDeliveryman { get; set; }

        [JsonProperty("inactiveDeliveryman")]
        public int InactiveDeliveryman { get; set; }

        [JsonProperty("Totaluser")]
        public int TotalUser { get; set; }

        [JsonProperty("TotalCustomer")]
        public int TotalCustomer { get; set; }
    }
}