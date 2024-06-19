using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Messages
{
    public class CreateMessageDTO
    {
        [Required]
        [MinLength(15)]
        public string MessageInfo { get; set; }
    }
}
