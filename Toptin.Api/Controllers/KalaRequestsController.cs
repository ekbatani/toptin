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
    public class KalaRequestsController : ControllerBase
    {
        private readonly IKalaRequest _IKalaRequest;

        public KalaRequestsController(IKalaRequest IKalaRequest)
        {
            _IKalaRequest = IKalaRequest;
        }

        // GET: api/KalaRequests
        [HttpGet]
        public async Task<IEnumerable<KalaRequest>> GetKalaRequest()
        {
            return await _IKalaRequest.GetAllAsync();
        }

        // GET: api/KalaRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KalaRequest>> GetKalaRequest(int id)
        {
            var kalaRequest = await _IKalaRequest.FindAsync(id);

            if (kalaRequest == null)
            {
                return NotFound();
            }

            return kalaRequest;
        }

        // PUT: api/KalaRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKalaRequest(int id, KalaRequest kalaRequest)
        {
            if (id != kalaRequest.KalaRequestId)
            {
                return BadRequest();
            }

            _IKalaRequest.Update(kalaRequest);

            try
            {
                await _IKalaRequest.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KalaRequestExists(id))
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

        // POST: api/KalaRequests
        [HttpPost]
        public async Task<ActionResult<KalaRequest>> PostKalaRequest(KalaRequest kalaRequest)
        {
            _IKalaRequest.Add(kalaRequest);
            await _IKalaRequest.SaveAsync();

            return CreatedAtAction("GetKalaRequest", new { id = kalaRequest.KalaRequestId }, kalaRequest);
        }

        // DELETE: api/KalaRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KalaRequest>> DeleteKalaRequest(int id)
        {
            var kalaRequest = await _IKalaRequest.FindAsync(id);
            if (kalaRequest == null)
            {
                return NotFound();
            }

            _IKalaRequest.Remove(kalaRequest);
            await _IKalaRequest.SaveAsync();

            return kalaRequest;
        }

        private bool KalaRequestExists(int id)
        {
            return _IKalaRequest.Any(id).Result;
        }
    }
}
