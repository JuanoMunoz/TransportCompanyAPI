using Microsoft.EntityFrameworkCore;
using TransportCompany.context;
using TransportCompany.Interface;
using TransportCompany.Models;

namespace TransportCompany.repository
{
    public class MessageRepository : IMessage
    {
        private readonly TransportDBContext _context; 
        public MessageRepository(TransportDBContext context) { _context = context; }  
        public async Task CreateMessageAsync(Message message)
        {
            await _context.Message.AddAsync(message);  
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            var messages = await _context.Message.Include(m=>m.User).ToListAsync();
            return messages;
        }

        public async Task<Message?> GetMessageByIdAsync(int id)
        {
            var message = await _context.Message.Include(m=>m.User).FirstOrDefaultAsync(x=>x.Id == id);
            if (message == null) { return null; }
            return message;
        }
    }
}
