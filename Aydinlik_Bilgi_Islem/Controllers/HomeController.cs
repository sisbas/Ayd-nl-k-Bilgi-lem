using Aydinlik_Bilgi_Islem.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aydinlik_Bilgi_Islem.Controllers
{
    public class HomeController : Controller
    {
        genelEntities ent = new genelEntities();
        // GET: Home
        public ActionResult Index()
        {                       
            return View();
        }
        [HttpPost]
        public ActionResult Index(GenelBilgi usr)
        {            
            ent.GenelBilgi.Add(usr);
            ent.SaveChanges();
            Response.Write("<script>alert('İşleminiz kısa süre içersinde gerçekleştirilecektir</script>'");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Administrator()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Administrator(GenelBilgi usr)
        {
            var bilgiler = (from i in ent.GenelBilgi.Select(i => i.ad)
                            select i).ToList();
            return View(bilgiler);
        }

    }
}