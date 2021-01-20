using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace ElsaWebApp.Controllers.DataAccess
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportMaterialController
    {
        private SchooldContext Context { get; }

        public SupportMaterialController(SchooldContext context)
        {
            Context = context;
        }
        
        [HttpGet("getAll")]
        public async Task<ActionResult<List<SupportMaterial>>> GetSupportMaterials()
        {
            var materials = await Context.SupportMaterials.ToListAsync();
            if (materials.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(materials);
        }

        [HttpGet("{id}")]
        public async Task<FileResult> GetSupportMaterial(int id)
        {
            var material = await Context.SupportMaterials.FirstOrDefaultAsync(supportMaterial => supportMaterial.MaterialId == id);
            return material == null
                ? null
                : new FileContentResult(material.BinaryData, System.Net.Mime.MediaTypeNames.Application.Octet)
                {
                    FileDownloadName = material.MaterialName
                };

        }

        [HttpPost("add")]
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