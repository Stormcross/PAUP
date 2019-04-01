using MvcStudenti2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcStudenti2.Controllers
{
    public class StudentController : Controller
    {
        //model nase forme
        private StudentiDb studenti = new StudentiDb();

        //ulazna stranica
        public ActionResult Index()
        {
            //prenosimo podatak iz kontrolera u view pomocu ViewBag objekta
            //The vieBag property eneblas you to dynamically share values from the controller to the view
            //It is a dynamic ojvect which means it has no pre-defined properties
            //You define the properties you want th ViewBag to have by simply adding them to the property
            //in the view you treieve those values by using same name for the roperty
            ViewBag.Title = "Baza studenata";
            return View();
        }

        //detalni podaci o studentu
        //ulazni parametar Id studenta, pozivamo kao Student/Detaljno/1, vidi: App_Start/RouteConfig.cs
        // sa int? bude proslo i bez "id", prima i null vrijednost
        public ActionResult Detaljno(int? id = 1)
        {
            //Naslov
            ViewBag.Title = "Detaljno o studentu";

            if (id == null)
            {
                //više na http://en.wikipedia.org/wiki/List_of_HTTP_status_codes
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //lista predefiniranih status kodova i tocno se zna koja je greska je za sto, npr 400 Bad Request
                //ako je metoda uredu ali tog studenta nema onda izbacue drugaciju vrstu greske prema potrebi
            }
            //lambda izraz - naci prog studenta koji ima Id jednak ulaznom parametur
            Student s = studenti.VratiListu().Find(x => x.Id == id); //Find je delegat, koji ima na sebi odredene metode
            //predicate tipa (potpis), sto je ulaz a sto izlaz, moze prihvatiti biblo koju metodu koja je s tim potpisom
            //sada je ulazni tip student trenutno, sada mozemo upotrijebiti lambda izrak(anonimna metoda)
            //x (tipa student, moze pisati bilo sto) =>(ulaz) x.Id==id (provjera argumenti, true ili false)
            if (s == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                //ili ovako krace napisano
                return HttpNotFound();
            }
            //vracamo view sa modelom kao ulaznim parametrom
            return View(s); //Student s
        }

        //stranica s popisom svih studenata
        public ActionResult Popis()
        {
            ViewBag.Title = "Popis studenata:";
            //vracamo view sa listom svih studenata ao ulaynim parametrom
            return View(studenti);
        }
        //**************************************************
        //Get metoda Azuriraj
        // prouciti http metode get,post,..
        //get citanje podataka
        //Post slanje podataka uvijek za azuriranje
        public ActionResult Azuriraj(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student s = studenti.VratiListu().Find(x => x.Id == id);
            if (s == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                //ili ovako krace napisano
                return HttpNotFound();
            }
            //vraćamo view sa modelom kao ulaznim parametrom
            ViewBag.Title = "Ažuriranje podataka o studentu";
            return View(s);
        }

        //preopterecujemo (overloading) Azuriraj() metodu
        //metoda koja se poziv a prilikom http POST requesta
        //atribut Bind je drugi sigurnosni mehanizam koji onemogučava zlonamjerno
        // mjenjanje atributa koji nisu za to predviđeni (over-posting)
        //post metoda Azuriraj
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(
            [Bind(Include ="Id,Ime,Prezime,Spol,Oib,GodinaStudija,DatumRodenja,RedovniStudent")] Student s)
        {
            if (ModelState.IsValid) //model state nam radi validaciju studenta
            {
                studenti.AzurirajStudenta(s);
                return RedirectToAction("Popis");
            }
            ViewBag.Title = "Ažuriranje podataka o studentu";
            return View(s);
        }
    }

}
