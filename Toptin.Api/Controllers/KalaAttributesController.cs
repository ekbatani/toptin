using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;

namespace Toptin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KalaAttributesController : ControllerBase
    {
        private readonly IKalaAttribute _iKalaAttribute;

        public KalaAttributesController(IKalaAttribute iKalaAttribute)
        {
            _iKalaAttribute = iKalaAttribute;
        }

        // GET: api/KalaAttribute
        [HttpGet]
        public async Task<IEnumerable<KalaAttribute>> GetKalaAttribute()
        {
            return await _iKalaAttribute.GetAllAsync();
        }

        // GET: api/KalaAttribute/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KalaAttribute>> GetKalaAttribute(int id)
        {
            var attribute = await _iKalaAttribute.FindAsync(id);

            if (attribute == null)
            {
                return NotFound();
            }

            return attribute;
        }

        // PUT: api/KalaAttribute/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKalaAttribute(int id, KalaAttribute kalaAttribute)
        {
            if (id != kalaAttribute.KalaAttributeId)
            {
                return BadRequest();
            }

            _iKalaAttribute.Update(kalaAttribute);

            try
            {
                await _iKalaAttribute.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KalaAttributeExists(id))
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

        // POST: api/KalaAttribute
        [HttpPost]
        public async Task<ActionResult<KalaAttribute>> PostKalaAttribute(KalaAttribute kalaAttribute)
        {
            _iKalaAttribute.Add(kalaAttribute);
            await _iKalaAttribute.SaveAsync();

            return CreatedAtAction("GetKalaAttribute", new { id = kalaAttribute.KalaAttributeId }, kalaAttribute);
        }

        // DELETE: api/KalaAttribute/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KalaAttribute>> DeleteKalaAttribute(int id)
        {
            var attribute = await _iKalaAttribute.FindAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }

            _iKalaAttribute.Remove(attribute);
            await _iKalaAttribute.SaveAsync();

            return attribute;
        }

        private bool KalaAttributeExists(int id)
        {
            return _iKalaAttribute.Any(id).Result;
        }
    }
}
