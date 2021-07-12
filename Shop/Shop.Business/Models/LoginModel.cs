using System.ComponentModel.DataAnnotations;

namespace Shop.Business.Models
{
    public class LoginModel
    {
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string Email { get; set; }

        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}")] 
        public string Password { get; set; }
    }
}
