using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDBJaanaPusa.Models;

namespace WebAppTilausDBJaanaPusa.Controllers
{
    public class AsiakkaatController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();
        // GET: Asiakkaat
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
                List<Asiakkaat> model = db.Asiakkaat.ToList(); //Luodaan lista Asiakkaat-luokan ilmentymistä nimellä model ja tallennetaan siihen tietokannan Asiakkaat 
                return View(model); //palautetaan yllä muodostettu lista-näkymä
            }

        }
        //Lisätään kirjautumisen tarkistus myös muihin metodeihin, koska jos älyää kirjoittaa osoiteriville erimerkiksi /Asiakkaat/Create, pääsee suoraan asiakkaan luonti-näkymään kirjautumatta sisään.

        //EDIT vaatii kaksi metodia. Ensimmäinen etsii tietyn id:n perusteella oikean asiakkaan ja alempi käy läpi mitä kenttiä on tulossa muokkauksesta ja tallentaa ne tietokantaan.
        public ActionResult Edit(int? id) //Edit-metodi jolle annetaan parametriksi id(id on määritelty View-kansion Index.cshtml-tiedostossa, tässä AsiakasID)
        {
            if (Session["UserName"] == null) //Tarkistaa, onko kirjauduttu sisään. Jos Session-UserName on tyhjä
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return RedirectToAction("login", "home"); //Palautetaan Login-näkymä HomeControllerista
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //Jos id:tä ei löydy, palautetaa BadRequest(serveri ei ymmärtänyt pyyntöä)
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id); //etsitään tietokannan (db) Shippers-taulusta primary keystä id:llä ja sijoitetaan tieto Asiakkaat-luokan asiakkaat-ilmentymään
                if (asiakkaat == null) return HttpNotFound(); //Jos id:tä ei löydy tietokannasta, palautetaan Error 404: Not Found
                ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", asiakkaat.Postinumero);
                return View(asiakkaat); //jos id löytyy, palautetaan näkymänä löydetty asiakas
            }
        }

        [HttpPost] //HttpPost tulee kun painetaan save-painiketta
        [ValidateAntiForgeryToken] 

        public ActionResult Edit([Bind(Include = "AsiakasID, Nimi, Osoite, Postinumero, Postitoimipaikat")] Asiakkaat asiakas) //haetaan kentät AsiakasID, Nimi jne. Tämä ei tarvitse login-tarkistusta, koska on sidottu Edit-metodiin
        {
            if (ModelState.IsValid) //jos kaikki on kunnossa
            {
                db.Entry(asiakas).State = EntityState.Modified; //tallennetaan tiedot kenttiin
                db.SaveChanges(); //tallennetaan muutokset tietokantaan
                ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", asiakas.Postinumero);
                return RedirectToAction("Index"); //palautetaan Index-näkymä
            }

            return View(asiakas); 
        }

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
                ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "AsiakasID, Nimi, Osoite, Postinumero, Postitoimipaikat")] Asiakkaat asiakas) //ei lisätä login-tarkistusta, koska on sidottu Create-metodiin
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaat.Add(asiakas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakas);
        }

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
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                if (asiakkaat == null) return HttpNotFound();
                return View(asiakkaat);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) //ei lisätä login-metodia, koska delete-näkymään ei pääse kirjautumatta. Silloin ei voi painaa poista-nappulaa.
        {
            try
            {
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                db.Asiakkaat.Remove(asiakkaat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }

        }

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
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                if (asiakkaat == null)
                {
                    return HttpNotFound();
                }
                return View(asiakkaat);
            }
        }
    }
}