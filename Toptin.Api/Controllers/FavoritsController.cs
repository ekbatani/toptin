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
    public class FavoritsController : ControllerBase
    {
        private readonly IFavorit _IFavorit;

        public FavoritsController(IFavorit IFavorit)
        {
            _IFavorit = IFavorit;
        }

        // GET: api/Favorits
        [HttpGet]
        public async Task<IEnumerable<Favorit>> GetFavorit()
        {
            return await _IFavorit.GetAllAsync();
        }

        // GET: api/Favorits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorit>> GetFavorit(int id)
        {
            var favorit = await _IFavorit.FindAsync(id);

            if (favorit == null)
            {
                return NotFound();
            }

            return favorit;
        }

        // PUT: api/Favorits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorit(int id, Favorit favorit)
        {
            if (id != favorit.FavoritId)
            {
                return BadRequest();
            }

            _IFavorit.Update(favorit);

            try
            {
                await _IFavorit.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoritExists(id))
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

        // POST: api/Favorits
        [HttpPost]
        public async Task<ActionResult<Favorit>> PostFavorit(Favorit favorit)
        {
            _IFavorit.Add(favorit);
            await _IFavorit.SaveAsync();

            return CreatedAtAction("GetFavorit", new { id = favorit.FavoritId }, favorit);
        }

        // DELETE: api/Favorits/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Favorit>> DeleteFavorit(int id)
        {
            var favorit = await _IFavorit.FindAsync(id);
            if (favorit == null)
            {
                return NotFound();
            }

            _IFavorit.Remove(favorit);
            await _IFavorit.SaveAsync();

            return favorit;
        }

        private bool FavoritExists(int id)
        {
            return _IFavorit.Any(id).Result;
        }
    }
}
