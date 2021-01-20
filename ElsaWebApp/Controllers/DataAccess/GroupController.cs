using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElsaWebApp.Controllers.DataAccess
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController
    {
        private SchooldContext Context { get; }

        public GroupController(SchooldContext context)
        {
            Context = context;
        }
        
        [HttpGet("getAll")]
        public async Task<ActionResult<List<UserGroup>>> GetGroups()
        {
            var groups = await Context.UserGroupSet.ToListAsync();
            if (groups.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserGroup>> GetGroup(int id)
        {
            var group = await Context.UserGroupSet.FirstOrDefaultAsync(group => group.GroupId == id);
            if (group == null)
                return new NotFoundResult();
            return new OkObjectResult(group);
        }

        [HttpPost]
        public async Task<ActionResult> InsertGroup(UserGroup group)
        {
            try
            {
                await Context.AddAsync(group);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch(Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("addRange")]
        public async Task<ActionResult> InsertGroup(List<UserGroup> groups)
        {
            try
            {
                await Context.AddRangeAsync(groups);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch(Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("assignRange")]
        public async Task<ActionResult> InsertGroup(List<UserGroups> groups)
        {
            try
            {
                await Context.AddRangeAsync(groups);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch(Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateGroup(UserGroup group)
        {
            try
            {
                await Task.Run(() => { Context.Update(group); });
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