using House_Utiliti_Service.Models;
using House_Utiliti_Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace House_Utiliti_Service.Controllers
{
    [Authorize]
    public class WorkAreasController : Controller
    {
        private readonly USDbContext db = new USDbContext();
        // GET: TestEntries
        public ActionResult Index()
        {
            var data = db.workAreas
                .Include(x => x.workerAreas.Select(y => y.Worker))
                .ToList();

            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.workers = db.workers.ToList();
            var data = new WorkAreaInputModel();
            data.workers.Add(new WorkerViewModel());
            return View(data);
        }
        [HttpPost]
        public ActionResult Create(WorkAreaInputModel model)
        {
            if (ModelState.IsValid)
            {
                var et = new WorkArea
                {
                    WorkAreaName = model.WorkAreaName,
                    Skill = model.Skill,
                    
                };
                foreach (var x in model.workers)
                {
                    et.workerAreas.Add(new WorkerArea { WorkerId = x.WorkerId });
                }
                
                db.workAreas.Add(et);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.workers = db.workers.ToList();

            return View(model);
        }
        //public JsonResult GetFee(int id)
        //{
        //    var t = db.workers.FirstOrDefault(x => x.WorkerId == id);
        //    return Json(t == null ? 0 : t.WorkerName,t.Phone,t.Payrate);
        //}
        public ActionResult CreateNewField(WorkAreaInputModel data)
        {
            ViewBag.workers = db.workers.ToList();
            data.workers.Add(new WorkerViewModel());
            return PartialView(data);
        }
    }
}