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
    public class PicturesController : ControllerBase
    {
        private readonly IPicture _IPicture;

        public PicturesController(IPicture IPicture)
        {
            _IPicture = IPicture;
        }

        // GET: api/Pictures
        [HttpGet]
        public async Task<IEnumerable<Picture>> GetPicture()
        {
            return await _IPicture.GetAllAsync();
        }

        // GET: api/Pictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var picture = await _IPicture.FindAsync(id);

            if (picture == null)
            {
                return NotFound();
            }

            return picture;
        }

        // PUT: api/Pictures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(int id, Picture picture)
        {
            if (id != picture.PictureId)
            {
                return BadRequest();
            }

            _IPicture.Update(picture);

            try
            {
                await _IPicture.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
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

        // POST: api/Pictures
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
            _IPicture.Add(picture);
            await _IPicture.SaveAsync();

            return CreatedAtAction("GetPicture", new { id = picture.PictureId }, picture);
        }

        // DELETE: api/Pictures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Picture>> DeletePicture(int id)
        {
            var picture = await _IPicture.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            _IPicture.Remove(picture);
            await _IPicture.SaveAsync();

            return picture;
        }

        private bool PictureExists(int id)
        {
            return _IPicture.Any(id).Result;
        }
    }
}
