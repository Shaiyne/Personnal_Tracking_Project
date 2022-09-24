using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonnalTrackingProject.Data;
using PersonnalTrackingProject.Models;
using System.Text.RegularExpressions;

namespace PersonnalTrackingProject.Controllers
{

    public class PersonnelController : BaseController
    {

        private readonly ApplicationDbContext _db;
        public PersonnelController(ApplicationDbContext db)
        {
            _db = db;
        }

        private readonly static string regexRule = "^[a-zA-Z ]*$";

        public IActionResult Index()
        {
            var obj2 = _db.Personals.Include(x => x.Department).ToList();
            return View(obj2);
        }

        [HttpGet]
        public IActionResult Adding()
        {
            viewbagdöndürme();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adding(Personnel objPersonnel)
        {
            if (!Regex.IsMatch(objPersonnel.PersonnelName, regexRule) || !(Regex.IsMatch(objPersonnel.PersonnelLastName, regexRule)))
            {
                viewbagdöndürme();
                ViewData["WrongAdding"] = "Invalid Personnel Name or Last Name! Try Again !";
                return View("Adding");
            }

            _db.Personals.Add(objPersonnel);
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ConnectionError", "İşlem sırasında hata oluştu , tekrar deneyin");
                return Redirect("Adding");
            }
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var personnel = _db.Personals.Find(id);
            if (personnel == null)
            {
                return RedirectToAction("Index");//error sayfasına gönder
            }
            return View("Edit", personnel);
        }

        [HttpPost]
        public IActionResult Edit(Personnel p)
        {
            var uPerson = _db.Personals.Find(p.PersonnelID);
            uPerson.PersonnelID = p.PersonnelID;
            uPerson.PersonnelName = p.PersonnelName;
            uPerson.PersonnelLastName = p.PersonnelLastName;
            uPerson.PersonnelAge = p.PersonnelAge;
            uPerson.DepartmentID = p.DepartmentID;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var pID = _db.Personals.Find(id);
            _db.Personals.Remove(pID);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void viewbagdöndürme()
        {
            var departmentList = (from d in _db.Departments
                                  select new SelectListItem()
                                  {
                                      Text = d.DepartmentName,
                                      Value = d.DepartmentID.ToString()
                                  }).ToList();
            departmentList.Insert(0, new SelectListItem()
            {
                Text = "Select Department",
                Value = string.Empty.ToString()
            });
            ViewBag.ListofDepartment = departmentList;
        }
    }
}
