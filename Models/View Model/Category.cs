using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class Category
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDelete { get; set; }
        public long createdBy { get; set; }
        public long modifiedBy { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        
    }
}