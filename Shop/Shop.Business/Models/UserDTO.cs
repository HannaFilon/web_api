using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Models
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string AddressDelivery { get; set; }
        public string PhoneNumber { get; set; }
        
        public UserDTO() 
        { 

        }

    }
}
