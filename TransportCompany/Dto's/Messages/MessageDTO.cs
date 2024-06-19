using System.ComponentModel.DataAnnotations;
using TransportCompany.Dto_s.Users;
using TransportCompany.Models;

namespace TransportCompany.Dto_s.Messages
{
    public class MessageDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string MessageInfo { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; }

        public UserMessageDTO User { get; set; }
    }
}
