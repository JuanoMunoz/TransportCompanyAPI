using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Users
{
    public class NewUserDTO
    {
        public string Username { get; set; }
        [Required]
        [EmailAddress]

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
