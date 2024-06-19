using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Users
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }    
    }
}
