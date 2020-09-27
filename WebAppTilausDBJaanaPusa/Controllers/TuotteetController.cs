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
    public class TuotteetController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Tuotteet
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
                return View(db.Tuotteet.ToList());
            }
        }

        // GET: Tuotteet/Details/5
        public ActionResult Details(int? id)
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
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                if (tuotteet == null)
                {
                    return HttpNotFound();
                }
                return View(tuotteet);
            }
        }

        // GET: Tuotteet/Create
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

        // POST: Tuotteet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva,Kuvalinkki")] Tuotteet tuotteet)
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Tuotteet.Add(tuotteet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tuotteet);
            }
        }

        // GET: Tuotteet/Edit/5
        public ActionResult Edit(int? id)
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
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                if (tuotteet == null)
                {
                    return HttpNotFound();
                }
                return View(tuotteet);
            }
        }

        // POST: Tuotteet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva,Kuvalinkki")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuotteet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Delete/5
        public ActionResult Delete(int? id)
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
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                if (tuotteet == null)
                {
                    return HttpNotFound();
                }
                return View(tuotteet);
            }
        }

        // POST: Tuotteet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try //halusin lisätä virheenkäsittelyn, jos yrittää poistaa sellaisen tuotteen, joka on jo käytössä
            {
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                db.Tuotteet.Remove(tuotteet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(); //jos yllä oleva tuotteen poisto ei onnistu, palauttaa delete-näkymän. En saanut näkymään ilmoitusta cshtml-tiedostossa.
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
