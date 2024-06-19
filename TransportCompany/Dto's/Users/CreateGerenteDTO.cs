using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Users
{
    public class CreateGerenteDTO
    {
        [Required]
        
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]

        public string Email { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public int BranchId { get; set; }
    }
}
