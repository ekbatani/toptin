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
    public class TagsController : ControllerBase
    {
        private readonly ITag _ITag;

        public TagsController(ITag ITag)
        {
            _ITag = ITag;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<IEnumerable<Tag>> GetTag()
        {
            return await _ITag.GetAllAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _ITag.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.TagId)
            {
                return BadRequest();
            }

            _ITag.Update(tag);

            try
            {
                await _ITag.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            _ITag.Add(tag);
            await _ITag.SaveAsync();

            return CreatedAtAction("GetTag", new { id = tag.TagId }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tag>> DeleteTag(int id)
        {
            var tag = await _ITag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _ITag.Remove(tag);
            await _ITag.SaveAsync();

            return tag;
        }

        private bool TagExists(int id)
        {
            return _ITag.Any(id).Result;
        }
    }
}
