using System.ComponentModel.DataAnnotations;

namespace Shop.Business.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string AddressDelivery { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
