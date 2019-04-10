using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQL_Student.Models;


namespace SQL_Student.Controllers
{
    public class HomeController : Controller
    {
        private StudentiDBEntities _dbStudenti = new StudentiDBEntities(); //Napravimo objekt za pristup bazi podataka
        // GET: Home
        public ActionResult Index()
        {
            return View(_dbStudenti.Studenti.ToList()); //modificiramo index za prikaz liste u index pregledu
            //Klikenmo na Index() i izradimo novi View
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var studentDetalji = (from s in _dbStudenti.Studenti
                                where s.Id == id
                                select s).First();
            return View(studentDetalji);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Student noviStudent)
        //[Bind(Exclude ="Id")] maknemo ID jer nam je Id generiran automatski s svakim novim studentom
        //kod Create napravimo novi objekt studenta
        {
            if (!ModelState.IsValid) //provjera ako je sve kako treba, ako nije uredu vracamo se na Dodaj novi page
            {
                return View();
            }

            _dbStudenti.Studenti.Add(noviStudent); //prema bazi podataka, u tabeli studenti, dodaj novi student
            //_dbStudenti.SaveChanges(); //spremimo promjene koje smo napravili
            //metoda dodana za prikaz greske
            try
            {
                _dbStudenti.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            return RedirectToAction("Index"); //Vracamo se na glavni preglednik Index
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            //izrada querry za upit na film
            var studentUredi = (from s in _dbStudenti.Studenti
                                where s.Id == id
                                select s).First(); 
            //sa upistom S "Student" u bazi DBStudenti 
            //u tablici studenti gdje je Id od s jednako id koji se trazi odaberi taj s, prvi koji dođe
            return View(studentUredi); //vrati vrijednost objekta studenta za redivanje
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Student studentUredi) //postavimo parametar "Student"
        {
            var orginalStudent = (from s in _dbStudenti.Studenti
                                  where s.Id == studentUredi.Id
                                  select s).First();

            //validacija na razini kontrolera -- strana server
            //eksplicitna validacija OIB-a
            if (!Oib.CheckOib(orginalStudent.Oib))
            {
                ModelState.AddModelError("Oib", "Neispravan Oib");
            }
            //provjera datuma
            if (orginalStudent.DatumRodenja>=DateTime.Now)
            {
                ModelState.AddModelError("DatumRodenja", "Datum rodenja treba biti manji od danasnjeg datuma");
            }

            if (!ModelState.IsValid) //provjera ako sve stima kako je zamisljeno
            {
                return View(orginalStudent);
            }

            _dbStudenti.Entry(orginalStudent).CurrentValues.SetValues(studentUredi);
            //uzimamo bazu DBStudenti, ulaz na orginalStudent i promjenimo svojstva kako ima studentUredi
            _dbStudenti.SaveChanges();//spremimo promjene
            return RedirectToAction("Index");
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var obrisiStudent = (from s in _dbStudenti.Studenti
                                 where s.Id == id
                                 select s).First();

            return View(obrisiStudent);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(Student obrisiStudent)
        {
            try
            {
                // TODO: Add delete logic here
                var obrisani = (from s in _dbStudenti.Studenti
                                where s.Id == obrisiStudent.Id
                                select s).First();

                _dbStudenti.Studenti.Remove(obrisani);
                _dbStudenti.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
