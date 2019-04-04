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
    public class StoresController : ControllerBase
    {
        private readonly IStore _IStore;

        public StoresController(IStore IStore)
        {
            _IStore = IStore;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<IEnumerable<Store>> GetStore()
        {
            return await _IStore.GetAllAsync();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = await _IStore.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }

        // PUT: api/Stores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, Store store)
        {
            if (id != store.StoreId)
            {
                return BadRequest();
            }

            _IStore.Update(store);

            try
            {
                await _IStore.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
            _IStore.Add(store);
            await _IStore.SaveAsync();

            return CreatedAtAction("GetStore", new { id = store.StoreId }, store);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Store>> DeleteStore(int id)
        {
            var store = await _IStore.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            _IStore.Remove(store);
            await _IStore.SaveAsync();

            return store;
        }

        private bool StoreExists(int id)
        {
            return _IStore.Any(id).Result;
        }
    }
}
