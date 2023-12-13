using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using harjoitustyoBackend.Models;
using harjoitustyoBackend.Services;

namespace harjoitustyoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly MessageServiceContext _context;
        private readonly IUserService _userService;

        public UsersController(MessageServiceContext context , IUserService userService)
        {
           // _context = context;
            _userService = userService;

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
          return Ok(await _userService.GetUsersAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            UserDTO dto = await _userService.GetUserAsync(id);
            if(dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
         
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            if(await _userService.UpdateUserAsync(user))
            {
                return NoContent();
            }
            return NotFound();

           
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            UserDTO? newUser = await _userService.NewUserAsync(user);
            if (newUser == null)
            {
                return Problem("Username not available. Choose different username");
            }
            //return CreatedAtAction("NewUser",newUser);
            return CreatedAtAction(nameof(GetUser),new { id = newUser.UserName }, newUser);
          
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if( await _userService.DeleteUserAsync(id))
            {
                return NoContent();
            }

            return NotFound();
            
        }

    }
}
