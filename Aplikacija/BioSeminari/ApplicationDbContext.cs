using Microsoft.AspNet.Identity.EntityFramework;
using MSDNRoles.Models;
using System.Data.Entity;

namespace MSDNRoles
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Seminar> Seminari { get; set; }
        public virtual DbSet<Predbiljezba> Predbiljezbe { get; set; }

        // getting error:
        // Multiple object sets per type are not supported. The object sets 'ApplicationUsers' and 'Users' can both contain instances of type 'MSDNRoles.Models.ApplicationUser'.
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        /*public virtual DbSet<Zaposlenik> Zaposlenici { get; set; }*/
    }
}