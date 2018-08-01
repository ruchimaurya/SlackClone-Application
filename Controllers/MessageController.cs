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
    [Route("/api/message")]
    public class MessageController : Controller
    {
        private readonly IMapper mapper;
        private readonly UsersDbContext context;
        public MessageController(IMapper mapper, UsersDbContext context)
        {
            this.context = context;
            this.mapper = mapper;

        }


        [HttpGet]
        public async Task<IEnumerable<MessageResource>> GetMessage(MessageResource messageResource)
        {
            var message = await context.Message.ToListAsync();
            return mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessageResource messageResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var message = mapper.Map<MessageResource, Message>(messageResource);
            context.Message.Add(message);
            await context.SaveChangesAsync();
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] MessageResource messageResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var message = await context.Message.FindAsync(id);
            if (message == null)
                return NotFound();
            mapper.Map<MessageResource, Message>(messageResource, message);
            await context.SaveChangesAsync();
            return Ok(message);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await context.Message.FindAsync(id);
            if (message == null)
                return NotFound();
            var messageResource = mapper.Map<Message, MessageResource>(message);
            return Ok(messageResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await context.Message.FindAsync(id);
            if (message == null)
                return NotFound();
            context.Remove(message);
            await context.SaveChangesAsync();
            return Ok(id);
        }
    }
}