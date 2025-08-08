using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using website.Models;

namespace website.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CreateCheckoutSession(int cartId)
        {
            var cart = _context.Carts
                .Include(c => c.Cartitems)
                .ThenInclude(ci => ci.Medicine)
                .FirstOrDefault(c => c.Cartid == cartId);

            if (cart == null || cart.Cartitems.Count == 0)
            {
                return NotFound("Cart not found or is empty.");
            }

            var lineItems = cart.Cartitems.Select(item =>
            {
                var price = item.Medicine?.Price ?? 0m;
                var discount = item.Medicine?.Discount ?? 0m;
                var priceAfterDiscount = price - (price * (discount / 100));

                return new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = priceAfterDiscount * 100, // Stripe uses cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Medicine?.Name
                        }
                    },
                    Quantity = (long?)item.Quantity
                };
            }).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Payment", new { cartId = cartId }, Request.Scheme),
                CancelUrl = Url.Action("Cancel", "Payment", new { cartId = cartId }, Request.Scheme)
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success(int cartId)
        {
            // Get the cart with items
            var cart = _context.Carts
                .Include(c => c.Cartitems)
                .FirstOrDefault(c => c.Cartid == cartId);

            if (cart != null && cart.Cartitems.Any())
            {
                // Remove all items from the cart
                _context.Cartitems.RemoveRange(cart.Cartitems);
                _context.SaveChanges();
            }

            ViewData["CartId"] = cartId;
            return View();
        }


        public IActionResult Cancel(int cartId)
        {
            ViewData["CartId"] = cartId;
            return View();
        }
    }
}
