using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class Count
    {
        public long Poducts { get; set; }
        public long ActivePoducts { get; set; }
        public long InactivePoducts { get; set; }
        public long Brands { get; set; }
        public long ActiveBrands { get; set; }
        public long InactiveBrands { get; set; }
        public long Categories { get; set; }
        public long ActiveCategories { get; set; }
        public long InactiveCategories { get; set; }
    }
}