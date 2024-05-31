using System.ComponentModel.DataAnnotations;

namespace ConsultasMedicas.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "RememberMe")]
        public string RememberMe { get; set; }
    }

}
