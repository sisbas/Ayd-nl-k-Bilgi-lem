using Aydinlik_Bilgi_Islem.Models.db;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Aydinlik_Bilgi_Islem.Controllers
{
    public class AdministratorController : Controller
    {
        private genelEntities4 db = new genelEntities4();
        
        // GET: Administrator
        public ActionResult Index()
        {
            return View(db.GenelBilgi.ToList());
        }
        public ActionResult Login()
        {
           return View();
        }
        [HttpPost]
        public ActionResult Login(Admin usr)
        {
            try
            {
                usr.id = 1;
                if (usr.user == db.Admin.FirstOrDefault().user && usr.password == db.Admin.FirstOrDefault().password)
                {
                    return RedirectToAction("Index");
                }
                else Response.Write("<script>alert('YANLIŞ')</script>");
            }
            catch (Exception e)
            {

                throw e;
            }         
            
            return RedirectToAction("Login");
        }
        // GET: Administrator/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenelBilgi genelBilgi = db.GenelBilgi.Find(id);
            if (genelBilgi == null)
            {
                return HttpNotFound();
            }
            return View(genelBilgi);
        }

        // GET: Administrator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ad,islemYapildimi,sikayet,ipAdres,macAdres,dateTime")] GenelBilgi genelBilgi)
        {
            if (ModelState.IsValid)
            {
                db.GenelBilgi.Add(genelBilgi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genelBilgi);
        }

        // GET: Administrator/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenelBilgi genelBilgi = db.GenelBilgi.Find(id);
            if (genelBilgi == null)
            {
                return HttpNotFound();
            }
            return View(genelBilgi);
        }

        // POST: Administrator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ad,islemYapildimi,sikayet,ipAdres,macAdres,dateTime")] GenelBilgi genelBilgi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genelBilgi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genelBilgi);
        }

        // GET: Administrator/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenelBilgi genelBilgi = db.GenelBilgi.Find(id);
            if (genelBilgi == null)
            {
                return HttpNotFound();
            }
            return View(genelBilgi);
        }

        // POST: Administrator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            GenelBilgi genelBilgi = db.GenelBilgi.Find(id);
            db.GenelBilgi.Remove(genelBilgi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
