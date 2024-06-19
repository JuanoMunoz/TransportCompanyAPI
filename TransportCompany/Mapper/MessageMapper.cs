using System.Linq.Expressions;
using TransportCompany.Dto_s.Messages;
using TransportCompany.Models;

namespace TransportCompany.Mapper
{
    public static class MessageMapper
    {
        public static Message toMessage(this CreateMessageDTO createMessageDTO, string userID)
        {
            return new Message
            {
                MessageInfo = createMessageDTO.MessageInfo,
                UserId = userID
            };
        }

        public static MessageDTO ToMessageDTO(this Message message)
        {
            return new MessageDTO
            {
                Id = message.Id,
                MessageInfo = message.MessageInfo,
                UserId = message.UserId,
                User = message.User.ToUserMessageDTO(),
            };
        }
    }
}
