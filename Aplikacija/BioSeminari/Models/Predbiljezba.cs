using System;
using System.ComponentModel.DataAnnotations;

namespace MSDNRoles.Models
{
    public class Predbiljezba
    {
        private ApplicationDbContext context = null;

        public Predbiljezba()
        {
            context = new ApplicationDbContext();
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Datum")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Ime polaznika je obavezan unos!")]
        [Display(Name = "Ime polaznika")]
        [StringLength(25, ErrorMessage = "Ime ne smije biti dulje od 25 znakova")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime polaznika je obavezan unos!")]
        [Display(Name = "Prezime polaznika")]
        [StringLength(25, ErrorMessage = "Prezime ne smije biti dulje od 25 znakova")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Adresa je obavezan unos!")]
        [Display(Name = "Adresa stanovanja")]
        [StringLength(100, ErrorMessage = "Adresa može sadržavati najviše 100 znakova")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Email adresa je obavezan unos!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Broj telefona ili mobitela je obavezan unos!")]
        [Display(Name = "Telefonski broj")]
        public string Telefon { get; set; }

        [Display(Name = "Seminar")]
        public int IdSeminar { get; set; }

        private Seminar seminar = null;

        public Seminar Seminar
        {
            get
            {
                if (seminar == null)
                {
                    seminar = context.Seminari.Find(this.IdSeminar);
                }
                return seminar;
            }
        }

        [Display(Name = "Status")]
        public bool Status { get; set; }
    }
}