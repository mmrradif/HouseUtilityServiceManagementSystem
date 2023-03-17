using House_Utiliti_Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace House_Utiliti_Service.Controllers
{
    [Authorize]
    public class WorkersController : Controller
    {
        USDbContext db = new USDbContext();
        public ActionResult Index()
        {
            return View(db.workers.Include(b => b.workerPayments).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateWorker()
        {
            return PartialView("_CreateWorker");
        }
        [HttpPost]
        public PartialViewResult CreateWorker(Worker w)
        {
            if (ModelState.IsValid)
            {
                db.workers.Add(w);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditWorker(int id)
        {
            var b = db.workers.First(x => x.WorkerId == id);
            return PartialView("_EditWorker", b);
        }
        [HttpPost]
        public PartialViewResult EditWorker(Worker w)
        {
            Thread.Sleep(4000);
            if (ModelState.IsValid)
            {
                db.Entry(w).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.workers.First(x => x.WorkerId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int workerId)
        {
            var b = new Worker { WorkerId = workerId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}