using System.ComponentModel.DataAnnotations;

namespace Shop.Business.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
    }
}
