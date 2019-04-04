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
    public class RequestsController : ControllerBase
    {
        private readonly IRequest _IRequest;

        public RequestsController(IRequest IRequest)
        {
            _IRequest = IRequest;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<IEnumerable<Request>> GetRequest()
        {
            return await _IRequest.GetAllAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _IRequest.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.RequestId)
            {
                return BadRequest();
            }

            _IRequest.Update(request);

            try
            {
                await _IRequest.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _IRequest.Add(request);
            await _IRequest.SaveAsync();

            return CreatedAtAction("GetRequest", new { id = request.RequestId }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _IRequest.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _IRequest.Remove(request);
            await _IRequest.SaveAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _IRequest.Any(id).Result;
        }
    }
}
