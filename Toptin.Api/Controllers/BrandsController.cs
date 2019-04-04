using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;

namespace Toptin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrand _IBrand;

        public BrandsController(IBrand iBrand)
        {
            _IBrand = iBrand;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<IEnumerable<Brand>> GetBrand()

        {
            return await _IBrand.GetAllAsync();
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _IBrand.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // PUT: api/Brands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.BrandId)
            {
                return BadRequest();
            }

            _IBrand.Update(brand);

            try
            {
                await _IBrand.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.Claims.First().Value;

            _IBrand.Add(brand);
            await _IBrand.SaveAsync();

            return Ok(brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> DeleteBrand(int id)
        {
            var brand = await _IBrand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _IBrand.Remove(brand);
            await _IBrand.SaveAsync();

            return brand;
        }

        private bool BrandExists(int id)
        {
            return _IBrand.Any(id).Result;
        }
    }
}
