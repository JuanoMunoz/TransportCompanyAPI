using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Statuses
{
    public class StatusDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; } = string.Empty;
    }
}
