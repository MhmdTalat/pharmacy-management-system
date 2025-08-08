using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using website.Models;

namespace website.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /UserAccount/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /UserAccount/Login
        [HttpPost]
        public IActionResult Login(Useraccount user)
        {
            if (ModelState.IsValid)
            {
                var matchedUser = _context.Useraccounts
                    .FirstOrDefault(u => u.Username == user.Username && u.Passwordhash == user.Passwordhash);

                if (matchedUser != null)
                {
                    HttpContext.Session.SetString("UserId", matchedUser.Userid.ToString()); // Save UserId for session
                    HttpContext.Session.SetString("Username", matchedUser.Username);
                    HttpContext.Session.SetString("Role", matchedUser.Role ?? "user");

                    // Redirect all users to Home/Index after login
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(user);
        }

        // GET: Admin Dashboard
        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "admin")
                return Unauthorized();

            ViewBag.User = HttpContext.Session.GetString("Username");
            return View();
        }

        // GET: Trader Dashboard
        public IActionResult TraderDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "trader")
                return Unauthorized();

            ViewBag.User = HttpContext.Session.GetString("Username");
            return View();
        }

        // GET: Normal User Dashboard
        public IActionResult UserDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "user")
                return Unauthorized();

            ViewBag.User = HttpContext.Session.GetString("Username");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
