using House_Utiliti_Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using House_Utiliti_Service.ViewModels;
using System.Threading;

namespace House_Utiliti_Service.Controllers
{
    [Authorize]
    public class WorkerPaymentsController : Controller
    {
        USDbContext db = new USDbContext();
        // GET: Trainees
        public ActionResult Index()
        {
            return View(db.workerPayments.Include(b => b.Worker).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.workers = db.workers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(PaymentInputModel t)
        {
            if (ModelState.IsValid)
            {
                var WorkerPayment = new WorkerPayment
                {
                    TotalWorkHour = t.TotalWorkHour,
                    TotalPayment = t.TotalPayment,
                    WorkerId = t.WorkerId
                };
                string ext = Path.GetExtension(t.WorkerPictur.FileName);
                string f = Guid.NewGuid() + ext;
                t.WorkerPictur.SaveAs(Server.MapPath("~/Uploads/") + f);
                WorkerPayment.WorkerPictur = f;
                db.workerPayments.Add(WorkerPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.workers = db.workers.ToList();
            return View(t);
        }
        public ActionResult Edit(int id)
        {
            var t = db.workerPayments.First(x => x.PaymentId == id);
            ViewBag.workers = db.workers.ToList();
            ViewBag.CurrentPic = t.WorkerPictur;
            return View(new PaymentEditModel { PaymentId = t.PaymentId, TotalWorkHour = t.TotalWorkHour, TotalPayment = t.TotalPayment, WorkerId = t.WorkerId });
        }
        [HttpPost]
        public ActionResult Edit(PaymentEditModel t)
        {
            Thread.Sleep(2000);
            var workerPayments = db.workerPayments.First(x => x.WorkerId == t.WorkerId);
            if (ModelState.IsValid)
            {

                workerPayments.TotalWorkHour = t.TotalWorkHour;
                workerPayments.TotalPayment = t.TotalPayment;
                if (t.WorkerPictur != null)
                {
                    string ext = Path.GetExtension(t.WorkerPictur.FileName);
                    string f = Guid.NewGuid() + ext;
                    t.WorkerPictur.SaveAs(Server.MapPath("~/Uploads/") + f);
                    workerPayments.WorkerPictur = f;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPic = workerPayments.WorkerPictur;
            ViewBag.Batches = db.workerPayments.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            return View(db.workerPayments.Include(x => x.Worker).First(x => x.PaymentId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            WorkerPayment t = new WorkerPayment { PaymentId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}