using House_Utiliti_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace House_Utiliti_Service.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        USDbContext db = new USDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.workers.ToList();
            return View();
        }
    }
}