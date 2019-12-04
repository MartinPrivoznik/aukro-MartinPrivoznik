using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataManagement.Models;
using Newtonsoft.Json;

namespace DataManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicUsersController : ControllerBase
    {
        private readonly AukroContext _context;

        public BasicUsersController()
        {
            _context = new AukroContext();
        }

        // GET: api/BasicUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasicUser>>> GetBasicUser()
        {
            //Get all users - include their sell stock
            return await _context.BasicUser.Include(e => e.SaleItem).ToListAsync();
        }

        // GET: api/BasicUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BasicUser>> GetBasicUser(int id)
        {
            var basicUser = await _context.BasicUser.FindAsync(id);

            if (basicUser == null)
            {
                return NotFound();
            }

            return basicUser;
        }

        // GET: api/BasicUsers/byusername?username='name'
        [HttpGet("byusername")]
        public async Task<ActionResult<BasicUser>> GetBasicUserByUserName(string username)
        {
            //Username is unique
            var basicUser = _context.BasicUser.Where(e => e.UserName == username).FirstOrDefault();

            if (basicUser == null)
            {
                return NotFound();
            }

            return basicUser;
        }

        [HttpGet("authenticate")]
        public async Task<ActionResult<string>> AuthenticateUser(string username, string password)
        {
            //Returns user in case it is in database
            var basicUser = _context.BasicUser.Where(usr => usr.UserName == username && usr.Password == password).Include(e => e.SaleItem).FirstOrDefault();

            if (basicUser == null)
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(basicUser);
        }

        // PUT: api/BasicUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasicUser(int id, BasicUser basicUser)
        {
            //Adjusting user
            if (id != basicUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(basicUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasicUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BasicUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BasicUser>> PostBasicUser(BasicUser basicUser)
        {
            //Register
            _context.BasicUser.Add(basicUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBasicUser", new { id = basicUser.Id }, basicUser);
        }

        // DELETE: api/BasicUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BasicUser>> DeleteBasicUser(int id)
        {
            var basicUser = await _context.BasicUser.FindAsync(id);
            if (basicUser == null)
            {
                return NotFound();
            }

            _context.BasicUser.Remove(basicUser);
            await _context.SaveChangesAsync();

            return basicUser;
        }

        private bool BasicUserExists(int id)
        {
            return _context.BasicUser.Any(e => e.Id == id);
        }
    }
}
