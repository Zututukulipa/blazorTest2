using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElsaWebApp.Controllers.DataAccess
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController
    {
        private SchooldContext Context { get; }

        public MessageController(SchooldContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PrivateMessage>>> GetMessages()
        {
            var messages = await Context.PrivateMessages.ToListAsync();
            if (messages.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrivateMessage>> GetMessage(int id)
        {
            var message = await Context.PrivateMessages.FirstOrDefaultAsync(message => message.MessageId == id);
            if (message == null)
                return new NotFoundResult();
            return new OkObjectResult(message);
        }

        [HttpPost]
        public async Task<ActionResult> InsertMessage(PrivateMessage message)
        {
            try
            {
                await Context.AddAsync(message);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMessage(PrivateMessage message)
        {
            try
            {
                await Task.Run(() => { Context.Update(message); });
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }
    }
}