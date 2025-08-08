using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using website.Models;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    // عرض محتويات السلة
    public IActionResult Index()
    {
        var cartItems = _context.Cartitems
            .Include(c => c.Medicine)
            .Include(c => c.Cart)
            .ToList();

        return View(cartItems);
    }

    // إضافة منتج للسلة
    public IActionResult AddToCart(decimal id)
    {
        if (id <= 0)
            return BadRequest("Invalid medicine ID.");

        decimal cartId = 1; // لاحقًا اربطه بالـ User

        var cart = _context.Carts.Find(cartId);
        if (cart == null)
        {
            cart = new Cart
            {
                Cartid = cartId,
                Createdat = DateTime.Now,
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        var medicine = _context.Medicines.Find(id);
        if (medicine == null)
            return NotFound("Medicine not found.");

        var existingItem = _context.Cartitems
            .FirstOrDefault(c => c.Cartid == cartId && c.Medicineid == id);

        if (existingItem != null)
        {
            existingItem.Quantity += 1;
        }
        else
        {
            var cartItem = new Cartitem
            {
                Cartid = cartId,
                Medicineid = id,
                Quantity = 1
            };
            _context.Cartitems.Add(cartItem);
        }

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("❗ Error while saving: " + ex.InnerException?.Message);
            return StatusCode(500, "An internal error occurred while adding to cart.");
        }

        return RedirectToAction("Index");
    }

    // حذف منتج من السلة
    public IActionResult RemoveFromCart(decimal id)
    {
        var cartItem = _context.Cartitems.Find(id);
        if (cartItem == null)
            return NotFound("Cart item not found.");

        _context.Cartitems.Remove(cartItem);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    // تفريغ السلة بالكامل
    public IActionResult ClearCart()
    {
        decimal cartId = 1; // لاحقًا اربطه بالـ User

        var items = _context.Cartitems.Where(c => c.Cartid == cartId);
        _context.Cartitems.RemoveRange(items);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
