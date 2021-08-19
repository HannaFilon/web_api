using System.Text.Json.Serialization;

namespace Shop.Business.ModelsDto
{
    public class UserDto
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AddressDelivery { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public string SecurityStamp { get; set; }
    }
}