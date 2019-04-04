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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _ICategory;

        public CategoriesController(ICategory ICategory)
        {
            _ICategory = ICategory;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await _ICategory.GetAllAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _ICategory.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _ICategory.Update(category);

            try
            {
                await _ICategory.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _ICategory.Add(category);
            await _ICategory.SaveAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _ICategory.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _ICategory.Remove(category);
            await _ICategory.SaveAsync();

            return category;
        }

        private bool CategoryExists(int id)
        {
            return _ICategory.Any(id).Result;
        }
    }
}
