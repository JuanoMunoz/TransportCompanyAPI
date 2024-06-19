using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Statuses
{
    public class UpdateStatusDTO
    {
        [Required]
        [MinLength(2)]
        public required string Name { get; set; }
    }
}
