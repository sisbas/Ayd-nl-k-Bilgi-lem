using Aydinlik_Bilgi_Islem.Models.db;
using System.Linq;
using System.Web.Mvc;

namespace Aydinlik_Bilgi_Islem.Controllers
{
    public class EditController : Controller
    {
        genelEntities6 ent = new genelEntities6();
        public ActionResult Administrator()
        {
            return View(ent.GenelBilgi);
        }
        // GET: Edit        
        public ActionResult Edit(long? id)
        {
            var c = ent.GenelBilgi.Where(i => i.id == id).FirstOrDefault();            
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(GenelBilgi gB)
        {                        
            var degistirilecek = ent.GenelBilgi.FirstOrDefault(i => i.id == gB.id);
            degistirilecek.islemYapildimi = gB.islemYapildimi;
            degistirilecek.ipAdres = gB.ipAdres;
            degistirilecek.macAdres = gB.macAdres;
            degistirilecek.ad = gB.ad;
            degistirilecek.sikayet = gB.sikayet;
            ent.SaveChanges();            
            return RedirectToAction("Administrator", "Edit");
        }
    }
}