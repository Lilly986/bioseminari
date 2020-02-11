using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MSDNRoles.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MSDNRoles.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationDbContext context = null;

        public UsersController()
        {
            this.context = new ApplicationDbContext();
        }

        // GET: Users
        public Boolean IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Users = context.Users.ToList();

                var roles = new List<string>();

                using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>()))
                {
                    foreach (var usr in context.Users.ToList())
                    {
                        if (usr.Roles.Count > 0)
                        {
                            var role = roleManager.FindById(usr.Roles.FirstOrDefault().RoleId);
                            roles.Add(role.Name);
                        }
                        else
                        {
                            roles.Add("Nema ulogu");
                        }
                    }

                    ViewBag.Roles = roles;

                    var user = User.Identity;
                    ViewBag.Name = user.Name;
                    //	ApplicationDbContext context = new ApplicationDbContext();
                    //	var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                    //var s=	UserManager.GetRoles(user.GetUserId());
                    ViewBag.displayMenu = "No";

                    if (IsAdminUser())
                    {
                        ViewBag.displayMenu = "Yes";
                    }
                    return View();
                }
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }

            return View();
        }

        public ActionResult Details(int? referenceId)
        {
            if (referenceId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = context.Users.FirstOrDefault(u => u.ReferenceId == referenceId.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            var seminar = (from s in context.Seminari
                           where s.ZaposlenikReferenceId == user.ReferenceId
                           select s).FirstOrDefault();

            if (seminar != null)
            {
                ViewBag.Kolegij = seminar.Naziv;
            }
            else
            {
                ViewBag.Kolegij = "Nema dodjeljenog seminara";
            }

            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>()))
            {
                if (user.Roles.Count > 0)
                {
                    var role = roleManager.FindById(user.Roles.FirstOrDefault().RoleId);
                    ViewBag.RoleName = role.Name;
                }
                else
                    ViewBag.RoleName = "Nema ulogu";
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? referenceId)
        {
            ViewBag.Name = new SelectList(context.Roles.Where(u => !u.Name.Contains("Admin"))
                                        .ToList(), "Name", "Name");

            if (referenceId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = context.Users.FirstOrDefault(u => u.ReferenceId == referenceId.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReferenceId,FirstName,LastName,Address,Email,Phone,UserName,")] ApplicationUser user)
        {
            ViewBag.Name = new SelectList(context.Roles.Where(u => !u.Name.Contains("Admin"))
                                        .ToList(), "Name", "Name");

            if (ModelState.IsValid)
            {
                using (var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    try
                    {
                        var usr = context.Users.FirstOrDefault(u => u.ReferenceId == user.ReferenceId);

                        usr.FirstName = user.FirstName;
                        usr.LastName = user.LastName;
                        usr.Address = user.Address;
                        usr.Email = user.Email;
                        usr.Phone = user.Phone;
                        usr.UserName = user.UserName;

                        UserManager.Update(user);
                        context.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException)
                    {
                        context.Entry(user).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    return RedirectToAction("Details", new { referenceId = user.ReferenceId });
                }
            }
            return View(user);
        }

        [HttpGet, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int referenceId)
        {
            ApplicationUser user = context.Users.Where(u => u.ReferenceId == referenceId).First();
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}