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
    public class AttributeCategoriesController : ControllerBase
    {
        private readonly IAttributeCategory _IAttributeCategory;

        public AttributeCategoriesController(IAttributeCategory IAttributeCategory)
        {
            _IAttributeCategory = IAttributeCategory;
        }

        // GET: api/AttributeCategories
        [HttpGet]
        public async Task<IEnumerable<AttributeCategory>> GetAttributeCategory()
        {
            return await _IAttributeCategory.GetAllAsync();
        }

        // GET: api/AttributeCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeCategory>> GetAttributeCategory(int id)
        {
            var attributeCategory = await _IAttributeCategory.FindAsync(id);

            if (attributeCategory == null)
            {
                return NotFound();
            }

            return attributeCategory;
        }

        // PUT: api/AttributeCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttributeCategory(int id, AttributeCategory attributeCategory)
        {
            if (id != attributeCategory.AttributeCategoryId)
            {
                return BadRequest();
            }

            _IAttributeCategory.Update(attributeCategory);

            try
            {
                await _IAttributeCategory.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttributeCategoryExists(id))
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

        // POST: api/AttributeCategories
        [HttpPost]
        public async Task<ActionResult<AttributeCategory>> PostAttributeCategory(AttributeCategory attributeCategory)
        {
            _IAttributeCategory.Add(attributeCategory);
            await _IAttributeCategory.SaveAsync();

            return CreatedAtAction("GetAttributeCategory", new { id = attributeCategory.AttributeCategoryId }, attributeCategory);
        }

        // DELETE: api/AttributeCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AttributeCategory>> DeleteAttributeCategory(int id)
        {
            var attributeCategory = await _IAttributeCategory.FindAsync(id);
            if (attributeCategory == null)
            {
                return NotFound();
            }

            _IAttributeCategory.Remove(attributeCategory);
            await _IAttributeCategory.SaveAsync();

            return attributeCategory;
        }

        private bool AttributeCategoryExists(int id)
        {
            return _IAttributeCategory.Any( id ).Result;
        }
    }
}
