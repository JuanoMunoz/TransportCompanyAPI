using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Users
{
    public class NewVisitorDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
