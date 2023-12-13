using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using harjoitustyoBackend.Models;
using harjoitustyoBackend.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using harjoitustyoBackend.Middleware;

namespace harjoitustyoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
      // private readonly MessageServiceContext _context;
        private readonly IMessageService _messageService;
        private readonly IUserAuthenticationService _authService;
        
        public MessagesController(IMessageService messageService, IUserAuthenticationService _authService )
        {
            _messageService = messageService;
            _authService = _authService;
        }

        // GET: api/Messages
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
          return Ok(await _messageService.GetMessagesAsync());
        }

        // GET: api/search/searchtext

        [HttpGet("search/{searchtext}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> SearchMessages(string searchtext)
        {
            return Ok(await _messageService.SearchMessagesAsync(searchtext));
        }


        [HttpGet("sent/{username}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMySentMessages(string username)
        {
            if(this.User.FindFirst(ClaimTypes.Name).Value == username) { 
            IEnumerable<MessageDTO> ?list = await _messageService.GetSentMessagesAsync(username);
            if(list== null)
            {
                return BadRequest();
            }
            return Ok(list);
            }
            return BadRequest();
        }
        [HttpGet("received/{username}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Message>>> GetMyReceivedMessages(string username)
        {
            if (this.User.FindFirst(ClaimTypes.Name).Value == username)
            {
                IEnumerable<MessageDTO>? list = await _messageService.GetReceivedMessagesAsync(username);
                if (list == null)
                {
                    return BadRequest();
                }
                return Ok(list);
            }
            return BadRequest();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDTO>> GetMessage(long id)
        {
          MessageDTO dto = await _messageService.GetMessageAsync(id); 
            if(dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutMessage(long id, MessageDTO message)
        {
            if (this.User.FindFirst(ClaimTypes.Name).Value == message.Sender)
            {
                if (id != message.Id)
                {
                    return BadRequest();


                    //MessageDTO nessageDTO = await _messageService.UpdateMessageAsync(message);

                    bool result = await _messageService.UpdateMessageAsync(message);

                    if (!result)
                    {
                        return NotFound(message);
                    }

                    return NoContent();
                }
            }
            return BadRequest();
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MessageDTO>> PostMessage(MessageDTO message)
        {
            if (this.User.FindFirst(ClaimTypes.Name).Value == message.Sender) { 
                MessageDTO newMessage = await _messageService.NewMessageAsync(message);
            
            if (newMessage == null)
            {
                return Problem();
            }

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
            }
            return BadRequest();
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(long id)
        {
            if(await _authService.isMyMessage(this.User.FindFirst(ClaimTypes.Name).Value, id))
            {

                if( await _messageService.DeleteMessageAsync(id))
                {
                return Ok();
                }
                return NotFound();
            }
            return BadRequest();

        }

        /*
        private bool MessageExists(long id)
        {
            return (_context.Messages?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        */
    }
}
