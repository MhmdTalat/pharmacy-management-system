using Microsoft.AspNetCore.Mvc;
using website.Models; // Replace with your actual namespace
   // If you're using DbContext

public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Customer
    public IActionResult Index()
    {
        var customers = _context.Customers.ToList();
        return View(customers);
    }

    // GET: /Customer/Details/5
    public IActionResult Details(int id)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.Customerid == id);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    // GET: /Customer/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Customer/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }

    // GET: /Customer/Edit/5
    public IActionResult Edit(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    // POST: /Customer/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Customer customer)
    {
        if (id != customer.Customerid)
            return BadRequest();

        if (ModelState.IsValid)
        {
            _context.Update(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }

    // GET: /Customer/Delete/5
    public IActionResult Delete(decimal id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    // POST: /Customer/DeleteConfirmed/5
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult DeleteConfirmed(decimal id)

    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
            return NotFound();

        _context.Customers.Remove(customer);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
