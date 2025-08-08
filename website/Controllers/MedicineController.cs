using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
using website.Models;

namespace website.Controllers
{
    public class MedicineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Medicine
        public async Task<IActionResult> Index()
        {
            var medicines = await _context.Medicines.ToListAsync();
            return View(medicines);
        }

        // GET: /Medicine/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
                return NotFound();

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.Medicineid == id);

            if (medicine == null)
                return NotFound();

            return View(medicine);
        }

        // GET: /Medicine/Create
        public IActionResult Create()
        {
            return View(new Medicine());
        }

        // POST: /Medicine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create(Medicine medicine, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Save the path in your model if needed
                   medicine.Imagepath = "/uploads/" + fileName;
                }

                try
                {
                    _context.Add(medicine);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("DbUpdateException: " + ex.InnerException?.Message);
                    // Or log it properly using a logging framework
                    ModelState.AddModelError("", "An error occurred while saving data: " + ex.InnerException?.Message);
                }

            }
            return View(medicine);
        }


        // GET: /Medicine/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
                return NotFound();

            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
                return NotFound();

            return View(medicine);
        }

        // POST: /Medicine/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, Medicine medicine, IFormFile? imageFile)
        {
            if (id != medicine.Medicineid)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine("wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        medicine.Imagepath = "/images/" + fileName;
                    }

                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(medicine);
        }


        // GET: /Medicine/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
                return NotFound();

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.Medicineid == id);

            if (medicine == null)
                return NotFound();

            return View(medicine);
        }

        // POST: /Medicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
                return NotFound();

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineExists(decimal id)
        {
            return _context.Medicines.Any(e => e.Medicineid == id);
        }
    }
}
