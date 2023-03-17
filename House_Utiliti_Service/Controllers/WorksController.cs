using House_Utiliti_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using House_Utiliti_Service.ViewModel;
using System.IO;
using System.Threading;

namespace House_Utiliti_Service.Controllers
{
    [Authorize]
    public class WorksController : Controller
    {
        USDbContext db = new USDbContext();
        public ActionResult Index()
        {
            return View(db.works.Include(b =>b.WorkArea).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.workareas = db.workAreas.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(WorkInputModel w)
        {
            if (ModelState.IsValid)
            {
                var work = new Work
                {
                    CustomerName = w.CustomerName,
                    CustomerAddress = w.CustomerAddress,
                    WorkDescription=w.WorkDescription,
                    StartDate=w.StartDate,
                    EndDate=w.EndDate,
                    WorkAreaId = w.WorkAreaId
                };
                db.works.Add(work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.workarea = db.workAreas.ToList();
            return View(w);
        }
        public ActionResult Edit(int id)
        {
            var t = db.works.First(x => x.WorkId == id);
            ViewBag.workAreas = db.workAreas.ToList();
            return View(new WorkEditModel { WorkId = t.WorkId, CustomerName = t.CustomerName, CustomerAddress = t.CustomerAddress,WorkDescription=t.WorkDescription,StartDate=t.StartDate,EndDate=t.EndDate ,WorkAreaId = t.WorkAreaId });
        }
        [HttpPost]
        public ActionResult Edit(WorkEditModel t)
        {
            Thread.Sleep(2000);
            var work = db.works.First(x => x.WorkId == t.WorkId);
            if (ModelState.IsValid)
            {


                work.CustomerName = t.CustomerName;
                work.CustomerAddress = t.CustomerAddress;
                work.WorkDescription = t.WorkDescription;
                work.StartDate = t.StartDate;
                work.EndDate = t.EndDate;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.works = db.works.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            return View(db.works.Include(x => x.WorkArea).First(x => x.WorkId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Work t = new Work { WorkId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}