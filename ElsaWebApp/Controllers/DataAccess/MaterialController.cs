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
    public class SupportMaterialController
    {
        private SchooldContext Context { get; }

        public SupportMaterialController(SchooldContext context)
        {
            Context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<SupportMaterial>>> GetSupportMaterials()
        {
            var materials = await Context.SupportMaterials.ToListAsync();
            if (materials.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(materials);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupportMaterial>> GetSupportMaterial(int id)
        {
            var material = await Context.SupportMaterials.FirstOrDefaultAsync(supportMaterial => supportMaterial.MaterialId == id);
            if (material == null)
                return new NotFoundResult();
            return new OkObjectResult(material);
        }

        [HttpPost]
        public async Task<ActionResult> InsertSupportMaterial(SupportMaterial supportMaterial)
        {
            try
            {
                await Context.AddAsync(supportMaterial);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch(Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSupportMaterial(SupportMaterial supportMaterial)
        {
            try
            {
                await Task.Run(() => { Context.Update(supportMaterial); });
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