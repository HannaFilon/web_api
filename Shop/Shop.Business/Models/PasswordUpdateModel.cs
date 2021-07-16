using System.ComponentModel.DataAnnotations;

namespace Shop.Business.Models
{
    public class PasswordUpdateModel
    {
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}", ErrorMessage = "Invalid New Password")]
        public string NewPassword { get; set; }
    }
}
