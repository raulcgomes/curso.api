using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Users
{
    public class RegisterViewModelInput
    {
        [Required(ErrorMessage = "The Login field is required")]
        public string Login { get; set;}

        [Required(ErrorMessage = "The Email field is required")]
        public string Email { get; set;}
        
        [Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set;}
    }
}
