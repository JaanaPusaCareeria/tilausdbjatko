using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDBJaanaPusa.Models;

namespace WebAppTilausDBJaanaPusa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() //Index-näkymän eli pääsivun kutsu
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
            }
            else ViewBag.LoggedStatus = "Kirjautuneena";
            return View(); //Palauttaa index-näkymän
        }

        public ActionResult Login()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                return View();
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautuneena";
                return View(); //palauttaa Login-näkymän (login.cshtml)
            }
            
        }

        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            TilausDBEntities1 db = new TilausDBEntities1();
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Kirjautuminen onnistui";
                ViewBag.LoggedStatus = "Kirjautuneena";
                Session["UserName"] = LoggedUser.UserName; //asettaa Session-olion parametriksi annetun UserNamen
                return RedirectToAction("Index", "Home"); //Onnistunut kirjautuminen johtaa Home/Index -sivulle
            }
            else
            {
                ViewBag.LoginMessage = "Kirjautuminen epäonnistui";
                ViewBag.LoggedStatus = "Kirjaudu sisään";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana"; //antaa virheilmoituksen
                return View("Login", LoginModel); //palauttaa Login-näkymän
            }
        }

        public ActionResult LogOut()
        {
            ViewBag.LoggedStatus = "Kirjaudu sisään";
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}