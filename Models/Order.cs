using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class Order
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }

        [JsonProperty("orderStateId")]
        public long OrderStateId { get; set; }

        [JsonProperty("orderRejectId")]
        public long OrderRejectId { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("thanaId")]
        public long ThanaId { get; set; }

        [JsonProperty("masterDepotId")]
        public long MasterDepotId { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("mobileNo")]
        public string MobileNo { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("deliveryAddress")]
        public string DeliveryAddress { get; set; }

        [JsonProperty("couponCode")]
        public string CouponCode { get; set; }

        [JsonProperty("couponDiscount")]
        public string CouponDiscount { get; set; }

        [JsonProperty("paymentMethod")]
        public int PaymentMethod { get; set; }

        [JsonProperty("deliveryCharge")]
        public decimal DeliveryCharge { get; set; }

        [JsonProperty("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("totalAmountAfterDiscount")]
        public decimal TotalAmountAfterDiscount { get; set; }

        [JsonProperty("totalDiscountAmount")]
        public decimal TotalDiscountAmount { get; set; }

        [JsonProperty("isPaid")]
        public bool IsPaid { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }


        [JsonProperty("calculatedVat")]
        public decimal CalculatedVat { get; set; }

        [JsonProperty("stateStatus")]
        public OrderStatus StateStatus { get; set; }

        [JsonProperty("rejectStatus")]
        public OrderRejectStatus RejectStatus { get; set; }

        [JsonProperty("thana")]
        public Thana Thana { get; set; }

        [JsonProperty("masterDepot")]
        public MasterDepot MasterDepot { get; set; }

        [JsonProperty("orderDetails")]
        public List<OrderDetail> OrderDetails { get; set; }

        [JsonProperty("assignedOrder")]
        public object AssignedOrder { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}