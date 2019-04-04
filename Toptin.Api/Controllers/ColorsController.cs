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
    public class ColorsController : ControllerBase
    {
        private readonly IColor _IColor;

        public ColorsController(IColor IColor)
        {
            _IColor = IColor;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<IEnumerable<Color>> GetColor()
        {
            return await _IColor.GetAllAsync();
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(int id)
        {
            var color = await _IColor.FindAsync(id);

            if (color == null)
            {
                return NotFound();
            }

            return color;
        }

        // PUT: api/Colors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(int id, Color color)
        {
            if (id != color.ColorId)
            {
                return BadRequest();
            }

            _IColor.Update(color);

            try
            {
                await _IColor.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
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

        // POST: api/Colors
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color color)
        {
            _IColor.Add(color);
            await _IColor.SaveAsync();

            return CreatedAtAction("GetColor", new { id = color.ColorId }, color);
        }

        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Color>> DeleteColor(int id)
        {
            var color = await _IColor.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }

            _IColor.Remove(color);
            await _IColor.SaveAsync();

            return color;
        }

        private bool ColorExists(int id)
        {
            return _IColor.Any(id).Result;
        }
    }
}
