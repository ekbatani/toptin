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
    public class KalaPicturesController : ControllerBase
    {
        private readonly IKalaPicture _IKalaPicture;

        public KalaPicturesController(IKalaPicture IKalaPicture)
        {
            _IKalaPicture = IKalaPicture;
        }

        // GET: api/KalaPictures
        [HttpGet]
        public async Task<IEnumerable<KalaPicture>> GetKalaPicture()
        {
            return await _IKalaPicture.GetAllAsync();
        }

        // GET: api/KalaPictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KalaPicture>> GetKalaPicture(int id)
        {
            var kalaPicture = await _IKalaPicture.FindAsync(id);

            if (kalaPicture == null)
            {
                return NotFound();
            }

            return kalaPicture;
        }

        // PUT: api/KalaPictures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKalaPicture(int id, KalaPicture kalaPicture)
        {
            if (id != kalaPicture.KalaPictureId)
            {
                return BadRequest();
            }

            _IKalaPicture.Update(kalaPicture);

            try
            {
                await _IKalaPicture.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KalaPictureExists(id))
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

        // POST: api/KalaPictures
        [HttpPost]
        public async Task<ActionResult<KalaPicture>> PostKalaPicture(KalaPicture kalaPicture)
        {
            _IKalaPicture.Add(kalaPicture);
            await _IKalaPicture.SaveAsync();

            return CreatedAtAction("GetKalaPicture", new { id = kalaPicture.KalaPictureId }, kalaPicture);
        }

        // DELETE: api/KalaPictures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KalaPicture>> DeleteKalaPicture(int id)
        {
            var kalaPicture = await _IKalaPicture.FindAsync(id);
            if (kalaPicture == null)
            {
                return NotFound();
            }

            _IKalaPicture.Remove(kalaPicture);
            await _IKalaPicture.SaveAsync();

            return kalaPicture;
        }

        private bool KalaPictureExists(int id)
        {
            return _IKalaPicture.Any(id).Result;
        }
    }
}
