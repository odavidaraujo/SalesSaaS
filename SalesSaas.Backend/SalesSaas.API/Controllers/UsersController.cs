using Microsoft.AspNetCore.Mvc;
using SalesSaaS.Domain.Entities;
using SalesSaaS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SalesSaaS.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context) {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
            return await _context.Users.Include(u => u.Companies).ToListAsync();
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id) {
            var user = await _context.Users.Include(u => u.Companies)
                                           .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound();

            return user;
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User updatedUser) {
            if (id != updatedUser.Id)
                return BadRequest();

            _context.Entry(updatedUser).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!_context.Users.Any(u => u.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id) {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
