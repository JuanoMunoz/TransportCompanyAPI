using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Users
{
    public class UserMessageDTO
    {

        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]

        public string Email { get; set; }
    }
}
