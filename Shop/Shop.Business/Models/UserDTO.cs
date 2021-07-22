namespace Shop.Business.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AddressDelivery { get; set; }
        public string PhoneNumber { get; set; }
        public string SecurityStamp { get; set; }
    }
}
