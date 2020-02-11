using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MSDNRoles.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MSDNRoles.Controllers
{
    public class SeminarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Seminar
        public ActionResult Index()
        {
            List<Seminar> seminari = db.Seminari.ToList();

            var user = User.Identity;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var roleList = UserManager.GetRoles();

            if (user.GetUserId() == null)
            {
                // Ako trenutni korisnik nije admin ni zaposlenik, skrati popis seminara samo na one
                // koji nisu popunjeni:
                seminari = seminari.Where(s => !s.Popunjen).ToList();
            }
            return View(seminari);
        }

        // GET: Create
        [HttpGet]
        [Authorize(Roles = "Admin, Zaposlenik")]
        public ActionResult Create()
        {
            var zaposlenici = new List<ApplicationUser>();
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>()))
            {
                foreach (var z in db.Users.ToList())
                {
                    if (z.Roles.Count > 0)
                    {
                        var role = roleManager.FindById(z.Roles.FirstOrDefault().RoleId);
                        if (role.Name == "Zaposlenik")
                            zaposlenici.Add(z);
                    }
                }
                ViewBag.Zaposlenici = zaposlenici;
            }
            return View(new Seminar() { Popunjen = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin, Manager")]
        public ActionResult Create(Seminar seminar)
        {
            if (ModelState.IsValidField("Datum"))
            {
                if (seminar.Datum < DateTime.Today)
                {
                    ModelState.AddModelError("", "Datum početka seminara ne smije biti u prošlosti.");
                }
                if (seminar.Datum >= DateTime.Today.AddYears(1))
                {
                    ModelState.AddModelError("", "Datum početka seminara ne smije biti veći za više od godinu dana.");
                }
            }

            if (ModelState.IsValid)
            {
                db.Seminari.Add(seminar);
                db.SaveChanges();
                return RedirectToAction("Index", "Seminar");
            }
            else
            {
                //povratak na formu
                return View();
            }
        }

        //GET : Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminari.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            var predavac = (from u in db.Users
                            where u.ReferenceId == seminar.ZaposlenikReferenceId
                            select u).FirstOrDefault();

            if (predavac != null)
            {
                ViewBag.Predavac = predavac.FullName();
            }
            else
            {
                ViewBag.Predavac = "Nema predavača";
            }

            if (User.IsInRole("Admin") || User.IsInRole("Zaposlenik"))
            {
                ViewBag.PredbiljezbePoSeminaru = (from p in db.Predbiljezbe
                                                  orderby p.Status
                                                  where p.IdSeminar == id
                                                  select p).ToList();
            }

            return View(seminar);
        }

        //GET : Edit
        [Authorize(Roles = "Admin, Zaposlenik")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminari.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }

            var zaposlenici = new List<ApplicationUser>();
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>()))
            {
                if (seminar.ZaposlenikReferenceId != null)
                {
                    var pred = (from p in db.Users
                                where p.ReferenceId == seminar.ZaposlenikReferenceId
                                select p).FirstOrDefault();

                    ViewBag.IzabraniPredavac = pred.FullName();
                }
                else
                {
                    ViewBag.IzabraniPredavac = "Nije izabran";
                }

                foreach (var z in db.Users.ToList())
                {
                    if (z.Roles.Count > 0)
                    {
                        var role = roleManager.FindById(z.Roles.FirstOrDefault().RoleId);
                        if (role.Name == "Zaposlenik")
                            zaposlenici.Add(z);
                    }
                }
                ViewBag.Zaposlenici = zaposlenici;
            }

            return View(seminar);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Zaposlenik")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Opis,Datum,Popunjen,ZaposlenikReferenceId")] Seminar seminar)
        {
            var zaposlenici = new List<ApplicationUser>();
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>()))
            {
                if (seminar.ZaposlenikReferenceId != null)
                {
                    var pred = (from p in db.Users
                                where p.ReferenceId == seminar.ZaposlenikReferenceId
                                select p).FirstOrDefault();

                    ViewBag.IzabraniPredavac = pred.FullName();
                }
                else
                {
                    ViewBag.IzabraniPredavac = "Nije izabran";
                }

                foreach (var z in db.Users.ToList())
                {
                    if (z.Roles.Count > 0)
                    {
                        var role = roleManager.FindById(z.Roles.FirstOrDefault().RoleId);
                        if (role.Name == "Zaposlenik")
                            zaposlenici.Add(z);
                    }
                }
                ViewBag.Zaposlenici = zaposlenici;
            }

            if (ModelState.IsValid)
            {
                db.Entry(seminar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = seminar.Id });
            }
            return View(seminar);
        }

        //GET : Delete

        [HttpGet, ActionName("Delete")]
        [Authorize(Roles = "Admin, Zaposlenik")]
        public ActionResult DeleteConfirmed(int id)
        {
            Seminar seminar = db.Seminari.Find(id);
            db.Seminari.Remove(seminar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public static int GetNewSeminarId()
        //{
        //    var lastId =
        //}

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