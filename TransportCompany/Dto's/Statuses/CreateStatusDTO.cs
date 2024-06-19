using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s
{
    public class CreateStatusDTO
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
