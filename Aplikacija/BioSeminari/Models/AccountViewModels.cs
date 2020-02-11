using System.ComponentModel.DataAnnotations;

namespace MSDNRoles.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Unos korisničkog imena je obavezno.")]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Unos lozinke je obavezno.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "Rola")]
        public string UserRoles { get; set; }

        [Required(ErrorMessage = "Email adresa je obavezna!")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Korisničko ime se mora postaviti!")]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lozinka se mora postaviti!")]
        [StringLength(100, ErrorMessage = "{0} mora imati najmanje {2} znakova.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinke nisu iste.")]
        public string ConfirmPassword { get; set; }

        // Custom fields:
        [Required]
        [Display(Name = "ID korisnika")]
        public int ReferenceId { get; set; }

        [Required(ErrorMessage = "Ime korisnika je obavezan unos!")]
        [Display(Name = "Ime")]
        [StringLength(25, ErrorMessage = "Ime može sadržavati maksimalno 25 znakova.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Prezime korisnika je obavezan unos!")]
        [Display(Name = "Prezime")]
        [StringLength(25, ErrorMessage = "Prezime može sadržavati maksimalno 25 znakova.")]
        public string LastName { get; set; }

        [Display(Name = "Adresa")]
        [StringLength(100, ErrorMessage = "Adresa može sadržavati maksimalno 100 znakova.")]
        public string Address { get; set; }

        [Display(Name = "Broj telefona")]
        [StringLength(50, ErrorMessage = "Maksimalno 50 znakova.")]
        public string Phone { get; set; }
    }

    //public class ResetPasswordViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }

    //    public string Code { get; set; }
    //}

    //public class ForgotPasswordViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }
    //}
}