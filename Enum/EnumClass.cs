using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Enum
{
    public class EnumClass
    {
        public enum OrderStateEnum
        {
            Pending = 1,
            Processing = 2,
            Packaging = 3,
            Shipped = 4,
            Delivered =5,
            Received =6,
            Cancelled=7
        }

        public enum UserTypeEnum
        {
            Admin = 1,
            SuperAdmin = 2,
            Customer = 3,
            MasterDepot = 4,
            DeliveryMan = 5
        }
    }
}