using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Users
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "The Login field is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }
    }
}
