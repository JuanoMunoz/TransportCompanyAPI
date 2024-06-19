using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Users
{
    public class VisitorDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]

        public string Email { get; set; }
    }
}
