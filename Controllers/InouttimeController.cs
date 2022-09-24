using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonnalTrackingProject.Data;
using PersonnalTrackingProject.Models;

namespace PersonnalTrackingProject.Controllers
{
    public class InouttimeController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public InouttimeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var obj = _db.Inouttimes.Include(x => x.Personnel).Include(y=>y.Personnel.Department).ToList();
            return View(obj);
        }
        public IActionResult Adding()
        {
            DropdownlistDöndürme();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adding(Inouttime objsInoutTime)
        {
            DropdownlistDöndürme();
            /*if (ModelState.Except(ModelState){

            }*/
            _db.Inouttimes.Add(objsInoutTime);
            _db.SaveChanges();
            return Redirect("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var iotID = _db.Inouttimes.Find(id);
            return View("Edit", iotID);
        }

        [HttpPost]
        public IActionResult Edit(Inouttime iot)
        {
            var uIOT = _db.Inouttimes.Find(iot.InOutTimeID);
            uIOT.InOutTimeID = iot.InOutTimeID;
            uIOT.EntryTime = iot.EntryTime;
            uIOT.OutTime = iot.OutTime;
            uIOT.PersonnelID = iot.PersonnelID;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var iotID = _db.Personals.Find(id);
            _db.Personals.Remove(iotID);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void DropdownlistDöndürme()
        {
            var personnelList = (from d in _db.Personals
                                  select new SelectListItem()
                                  {
                                      Text = d.PersonnelName,
                                      Value = d.PersonnelID.ToString()
                                  }).ToList();
            personnelList.Insert(0, new SelectListItem()
            {
                Text = "Select Personnel",
                Value = string.Empty.ToString()
            });
            ViewBag.ListofPersonnel = personnelList;
        }
    }
}
