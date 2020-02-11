using MSDNRoles.Models;
using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MSDNRoles.Controllers
{
    public class PredbiljezbaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index(string searchQuery, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Prezime_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Datum_asc" ? "Datum_desc" : "Datum_asc";

            if (searchQuery != null)
            {
                page = 1;
            }
            else
            {
                searchQuery = currentFilter;
            }
            ViewBag.CurrentFilter = searchQuery;

            var model = (from p in db.Predbiljezbe
                         orderby p.Prezime
                         select p).AsQueryable();

            // Pretraga:
            if (String.IsNullOrEmpty(searchQuery) == false)
            {
                model = model.Where(p =>
                    p.Ime.Contains(searchQuery) || p.Prezime.Contains(searchQuery) ||
                    (p.Ime + " " + p.Prezime).Contains(searchQuery));
            }

            switch (sortOrder)
            {
                case "Prezime_desc":
                    model = model.OrderByDescending(p => p.Prezime);
                    break;

                case "Datum":
                    model = model.OrderBy(p => p.Datum);
                    break;

                case "Datum_desc":
                    model = model.OrderByDescending(p => p.Datum);
                    break;

                default:
                    break;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            ViewBag.searchQuery = searchQuery;
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predbiljezba predbiljezba = db.Predbiljezbe.Find(id);
            if (predbiljezba == null)
            {
                return HttpNotFound();
            }
            return View(predbiljezba);
        }

        public ActionResult Create(int? id)
        {
            var seminar = db.Seminari.Find(id);
            ViewBag.Seminar = seminar;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,Prezime,Adresa,Email,Telefon,IdSeminar,Status")] Predbiljezba predbiljezba)
        {
            if (ModelState.IsValid)
            {
                predbiljezba.Datum = DateTime.Now;
                db.Predbiljezbe.Add(predbiljezba);
                db.SaveChanges();
                ViewBag.Poruka = "Uspiješno ste se predbilježili!";
                return RedirectToAction("Details", new { id = predbiljezba.Id });
            }

            return View(predbiljezba);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predbiljezba predbiljezba = db.Predbiljezbe.Find(id);
            if (predbiljezba == null)
            {
                return HttpNotFound();
            }
            ViewBag.IzabraniSeminar = predbiljezba.Seminar.Naziv;
            var seminari = db.Seminari.Where(s => !s.Popunjen);
            ViewBag.SeminariDrop = seminari;
            ViewBag.SeminarNaziv = db.Seminari.First(s => s.Id == predbiljezba.IdSeminar).Naziv;
            return View(predbiljezba);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Datum,Ime,Prezime,Adresa,Email,Telefon,IdSeminar,Status")] Predbiljezba predbiljezba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(predbiljezba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = predbiljezba.Id });
            }
            var seminari = db.Seminari.Where(s => !s.Popunjen);
            ViewBag.SeminariDrop = seminari.ToList();
            ViewBag.IzabraniSeminar = predbiljezba.Seminar.Naziv;
            return View(predbiljezba);
        }

        [HttpGet, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Predbiljezba predbiljezba = db.Predbiljezbe.Find(id);
            db.Predbiljezbe.Remove(predbiljezba);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
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