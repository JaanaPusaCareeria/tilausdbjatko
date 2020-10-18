using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDBJaanaPusa.Models;
using WebAppTilausDBJaanaPusa.ViewModels;

namespace WebAppTilausDBJaanaPusa.Controllers
{
    public class TilauksetController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Tilaukset
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
                var tilaukset = db.Tilaukset.Include(t => t.Asiakkaat).Include(t => t.Postitoimipaikat);        
                return View(tilaukset.ToList());
            }
        }

        // GET: Tilaukset/Details/5
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
                {
                    return HttpNotFound();
                }
                return View(tilaukset);
            }
        }

        // GET: Tilaukset/Create
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
                ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi");
                ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka");
                return View();
            }
        }

        // POST: Tilaukset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(tilaukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilaukset/Edit/5
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
                ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
                return View(tilaukset);
            }
        }

        // POST: Tilaukset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilaukset/Delete/5
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
                {
                    return HttpNotFound();
                }
                return View(tilaukset);
            }
        }

        // POST: Tilaukset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                db.Tilaukset.Remove(tilaukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult TilausOtsikot()
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                var tilaukset = db.Tilaukset.Include(t => t.Asiakkaat).Include(t => t.Postitoimipaikat);
                return View(tilaukset.ToList());
            }
        }

        public ActionResult _TilausRivit(int? tilausid)
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                var tilausRivilista  = from tr in db.Tilausrivit
                                       join t in db.Tuotteet on tr.TuoteID equals t.TuoteID
                                       where tr.TilausID == tilausid
                                       select new TilausRivit
                                       {
                                           TilausID = (int)tr.TilausID,
                                           TilausriviID = tr.TilausriviID,
                                           TuoteID = t.TuoteID,
                                           TuoteNimi = t.Nimi,
                                           Maara = (int)tr.Maara,
                                           Ahinta = (float)t.Ahinta
                                       };

                    return PartialView(tilausRivilista);
            }
        }

        public ActionResult TilaustenMaara()
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                string viikonPaivaLista;
                string tilausMaaraLista;

                List<TilauksetMape> tilauksetArkipaivinaLista = new List<TilauksetMape>();

                var tilauksetArki = from ta in db.TilauksetArkipaivina
                                    orderby ta.tilausmaara
                                    select ta;

                foreach (TilauksetArkipaivina tilaukset in tilauksetArki)
                {
                    TilauksetMape tilaus = new TilauksetMape();
                    tilaus.ViikonPaiva = tilaukset.viikonpaiva;
                    tilaus.TilausMaara = (int)tilaukset.tilausmaara;
                    tilauksetArkipaivinaLista.Add(tilaus);
                }

                viikonPaivaLista = "'" + string.Join("','", tilauksetArkipaivinaLista.Select(n => n.ViikonPaiva).ToList()) + "'";
                tilausMaaraLista = string.Join(",", tilauksetArkipaivinaLista.Select(n => n.TilausMaara).ToList());

                ViewBag.viikonPaiva = viikonPaivaLista;
                ViewBag.tilausMaara = tilausMaaraLista;

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
