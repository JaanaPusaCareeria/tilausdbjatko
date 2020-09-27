using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDBJaanaPusa.Models;

namespace WebAppTilausDBJaanaPusa.Controllers
{
    public class PostitoimipaikatController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Postitoimipaikat
        public ActionResult Index()
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                return View(db.Postitoimipaikat.ToList());
            }
        }

        // GET: Postitoimipaikat/Details/5
        public ActionResult Details(string id)
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
                if (postitoimipaikat == null)
                {
                    return HttpNotFound();
                }
                return View(postitoimipaikat);
            }
        }

        // GET: Postitoimipaikat/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                return View();
            }
        }

        // POST: Postitoimipaikat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat postitoimipaikat)
        {
                if (ModelState.IsValid)
                {
                    db.Postitoimipaikat.Add(postitoimipaikat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(postitoimipaikat);
        }

        // GET: Postitoimipaikat/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
                if (postitoimipaikat == null)
                {
                    return HttpNotFound();
                }
                return View(postitoimipaikat);
            }
        }

        // POST: Postitoimipaikat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat postitoimipaikat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postitoimipaikat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postitoimipaikat);
        }

        // GET: Postitoimipaikat/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
                if (postitoimipaikat == null)
                {
                    return HttpNotFound();
                }
                return View(postitoimipaikat);
            }
        }

        // POST: Postitoimipaikat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
                db.Postitoimipaikat.Remove(postitoimipaikat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }

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
