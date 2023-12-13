using harjoitustyoBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace harjoitustyoBackend.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        MessageServiceContext _context;

        public MessageRepository(MessageServiceContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteMessageAsync(Message message)
        {
            if (message == null)
            {
                return false;
            }
            else
            {
                _context.Remove(message);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Message?> GetMessageAsync(long id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _context.Messages.Where(x =>x.Recipient==null).OrderByDescending(x=>x.Id).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Message>> SearchMessages(string searchtext)
        {
            return await _context.Messages.Where(x => x.Recipient == null).Where(x=>x.Title.Contains(searchtext)|| x.Body.Contains(searchtext)).OrderByDescending(x => x.Id).Take(10).ToListAsync();
        }


        public async Task<IEnumerable<Message>> GetReceivedMessagesAsync(User user)
        {
            return await _context.Messages.Where(x => x.Recipient == user).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetSentMessagesAsync(User user)
        {
            return await _context.Messages.Where(x => x.Sender == user).ToListAsync();
        }

        public async Task<Message> NewMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
