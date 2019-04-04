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
    public class RequestDetailsController : ControllerBase
    {
        private readonly IRequestDetail _IRequestDetail;

        public RequestDetailsController(IRequestDetail IRequestDetail)
        {
            _IRequestDetail = IRequestDetail;
        }

        // GET: api/RequestDetails
        [HttpGet]
        public async Task<IEnumerable<RequestDetail>> GetRequestDetail()
        {
            return await _IRequestDetail.GetAllAsync();
        }

        // GET: api/RequestDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDetail>> GetRequestDetail(int id)
        {
            var requestDetail = await _IRequestDetail.FindAsync(id);

            if (requestDetail == null)
            {
                return NotFound();
            }

            return requestDetail;
        }

        // PUT: api/RequestDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestDetail(int id, RequestDetail requestDetail)
        {
            if (id != requestDetail.RequestDetailId)
            {
                return BadRequest();
            }

            _IRequestDetail.Update(requestDetail);

            try
            {
                await _IRequestDetail.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestDetailExists(id))
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

        // POST: api/RequestDetails
        [HttpPost]
        public async Task<ActionResult<RequestDetail>> PostRequestDetail(RequestDetail requestDetail)
        {
            _IRequestDetail.Add(requestDetail);
            await _IRequestDetail.SaveAsync();

            return CreatedAtAction("GetRequestDetail", new { id = requestDetail.RequestDetailId }, requestDetail);
        }

        // DELETE: api/RequestDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestDetail>> DeleteRequestDetail(int id)
        {
            var requestDetail = await _IRequestDetail.FindAsync(id);
            if (requestDetail == null)
            {
                return NotFound();
            }

            _IRequestDetail.Remove(requestDetail);
            await _IRequestDetail.SaveAsync();

            return requestDetail;
        }

        private bool RequestDetailExists(int id)
        {
            return _IRequestDetail.Any(id).Result;
        }
    }
}
