using System;

namespace website.Models
{
    public class CartItemViewModel
    {
        public decimal CartItemId { get; set; }
        public string MedicineName { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal Quantity { get; set; }
    }
}
