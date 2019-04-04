using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;

namespace Toptin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KalasController : ControllerBase
    {
        private readonly IKala _IKala;

        public KalasController(IKala IKala)
        {
            _IKala = IKala;
        }

        // GET: api/Kalas
        [HttpGet]
        public async Task<IEnumerable<Kala>> GetKala()
        {
            return await _IKala.GetAllAsync();
        }

        // GET: api/Kalas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kala>> GetKala(int id)
        {
            var kala = await _IKala.FindAsync(id);

            if (kala == null)
            {
                return NotFound();
            }

            return kala;
        }

        // PUT: api/Kalas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKala(int id, Kala kala)
        {
            if (id != kala.KalaId)
            {
                return BadRequest();
            }

            _IKala.Update(kala);

            try
            {
                await _IKala.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KalaExists(id))
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

        // POST: api/Kalas
        [HttpPost]
        public async Task<ActionResult<Kala>> PostKala(Kala kala)
        {
            _IKala.Add(kala);
            await _IKala.SaveAsync();

            return CreatedAtAction("GetKala", new { id = kala.KalaId }, kala);
        }

        // DELETE: api/Kalas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kala>> DeleteKala(int id)
        {
            var kala = await _IKala.FindAsync(id);
            if (kala == null)
            {
                return NotFound();
            }

            _IKala.Remove(kala);
            await _IKala.SaveAsync();

            return kala;
        }

        private bool KalaExists(int id)
        {
            return _IKala.Any(id).Result;
        }
    }
}
