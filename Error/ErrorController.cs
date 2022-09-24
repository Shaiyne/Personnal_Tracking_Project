using Microsoft.AspNetCore.Mvc;

namespace PersonnalTrackingProject.Error
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorHandling()
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                return Redirect("/Home/Error");
            }
            else if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/User/Error");
            }
            return View();
        }
    }
}
