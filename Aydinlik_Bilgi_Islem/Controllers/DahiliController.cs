using Aydinlik_Bilgi_Islem.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aydinlik_Bilgi_Islem.Controllers
{
    public class DahiliController : Controller
    {
        genelEntities4 ent = new genelEntities4();
        // GET: Dahili
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AjaxGetJsonData()
        {
            var list = ent.dahiliNum.ToList();
            return Json(list);
        }
    }
}