using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonnalTrackingProject.Data;
using PersonnalTrackingProject.Models;
using System.Text.RegularExpressions;

namespace PersonnalTrackingProject.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly static string regexRule = "^[a-zA-Z ]*$";
        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Department> objsDepartment = _db.Departments;
            return View(objsDepartment);
        }
        public IActionResult Adding()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adding(Department objsDepartment)
        {
            if (!Regex.IsMatch(objsDepartment.DepartmentName, regexRule))
            {
                ViewData["WrongAdding"] = "Invalid Department Name !Please do not enter a number in Department Name !";
                return View("Adding");
            }
            _db.Departments.Add(objsDepartment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dID = _db.Departments.Find(id);
            return View("Edit", dID);
        }

        [HttpPost]
        public IActionResult Edit(Department d)
        {
            var uDepartment = _db.Departments.Find(d.DepartmentID);
            uDepartment.DepartmentID = d.DepartmentID;
            uDepartment.DepartmentName = d.DepartmentName;
            uDepartment.DepartmentLocation = d.DepartmentLocation;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var dID = _db.Departments.Find(id);
            _db.Departments.Remove(dID);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
