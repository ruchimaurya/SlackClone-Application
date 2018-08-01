using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SlackClone.usersDB;
using SlackClone.Controllers.Resources;
using SlackClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace SlackClone.Controllers
{
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly UsersDbContext context;
        public UsersController(IMapper mapper, UsersDbContext context)
        {
            this.context = context;
            this.mapper = mapper;

        }


        [HttpGet]
        public async Task<IEnumerable<UsersResource>> GetUsers(UsersResource usersResource)
        {
            var users = await context.Users.ToListAsync();
            return mapper.Map<IEnumerable<Users>, IEnumerable<UsersResource>>(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] UsersResource usersResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = mapper.Map<UsersResource, Users>(usersResource);
            context.Users.Add(users);
            await context.SaveChangesAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers(int id, [FromBody] UsersResource usersResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = await context.Users.FindAsync(id);
            if (users == null)
                return NotFound();
            mapper.Map<UsersResource, Users>(usersResource, users);
            await context.SaveChangesAsync();
            return Ok(users);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var users = await context.Users.FindAsync(id);
            if (users == null)
                return NotFound();
            var usersResource = mapper.Map<Users, UsersResource>(users);
            return Ok(usersResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await context.Users.FindAsync(id);
            if (users == null)
                return NotFound();
            context.Remove(users);
            await context.SaveChangesAsync();
            return Ok(id);
        }
    }
}