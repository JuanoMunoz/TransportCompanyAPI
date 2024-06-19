using TransportCompany.Models;

namespace TransportCompany.Interface
{
    public interface IMessage
    {
        public Task<List<Message>> GetAllMessagesAsync();
        public Task CreateMessageAsync(Message message);

        public Task<Message?> GetMessageByIdAsync(int id);
    }
}
