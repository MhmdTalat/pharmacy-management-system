using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
using website.Models;

public class CartItemController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int cartId)
    {
        var items = await _context.Cartitems
            .Where(i => i.Cartid == cartId)
            .Include(i => i.Medicineid)
            .ToListAsync();
        ViewBag.CartId = cartId;
        return View(items);
    }

    public IActionResult Create(int cartId)
    {
        ViewBag.CartId = cartId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cartitem item)
    {
        if (ModelState.IsValid)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { cartId = item.Cartid });
        }
        return View(item);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _context.Cartitems.FindAsync(id);
        if (item == null)
            return NotFound();

        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(decimal id, Cartitem item)
    {
        if (id != item.Cartid)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { cartId = item.Cartid });
        }
        return View(item);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Cartitems.FindAsync(id);
        if (item == null)
            return NotFound();

        return View(item);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.Cartitems.FindAsync(id);
        if (item != null)
        {
            _context.Cartitems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index), new { cartId = item.Cartid });
    }
}
