using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MSDNRoles.Models
{
    public class Seminar
    {
        private ApplicationDbContext context = null;

        public Seminar()
        {
            context = new ApplicationDbContext();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv seminara je obavezan unos.")]
        [StringLength(100, ErrorMessage = "Naziv seminara može imati najviše 100 znakova.")]
        public string Naziv { get; set; }

        public string Opis { get; set; }

        [Required(ErrorMessage = "Datum i vrijeme seminara mora se postaviti.")]
        public DateTime Datum { get; set; }

        public int? ZaposlenikReferenceId { get; set; }

        [Required]
        public bool Popunjen { get; set; }

        public int BrojPredbiljezbi
        {
            get
            {
                return context.Predbiljezbe.Where(p => p.IdSeminar == this.Id).Count();
            }
        }

        //lista predbiljezbi po seminaru
        //public virtual ICollection<Predbiljezba> Predbiljezbe { get; set; }

        //public Seminar()
        //{
        //    Predbiljezbe = new HashSet<Predbiljezba>();
        //}
    }
}