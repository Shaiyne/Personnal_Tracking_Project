using Microsoft.AspNetCore.Mvc;
using PersonnalTrackingProject.Data;
using PersonnalTrackingProject.Models;

namespace PersonnalTrackingProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }
        public IActionResult SignUp() {

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("Index");
        }

        //[ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {

            var user = _db.Users.FirstOrDefault(w => w.Email.Equals(email) && w.UserPassword.Equals(password));
            if (user != null)
            {
                HttpContext.Session.SetInt32("id", user.UserID);
                HttpContext.Session.SetString("fullname", user.UserName + " " + user.UserLastName);
                return Redirect("/Home/Index");
            }
            else if (user==null)
            {
                ViewData["WrongLogin"] = "Invalid Email or Password ! Please Try Again or Sign Up";
                return View("Index");
            }

            return Redirect("Index");
        }


        public async Task<IActionResult> Register(User objUser)
        {
            await _db.Users.AddAsync(objUser);
            await _db.SaveChangesAsync();
            return Redirect("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
