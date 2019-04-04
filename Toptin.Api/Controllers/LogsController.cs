using System;
using System.Collections.Generic;
using System.Data;
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
    public class LogsController : ControllerBase
    {
        private readonly ILog _Ilog;

        public LogsController(ILog iLog)
        {
            _Ilog = iLog;
        }

        // GET: api/Logs
        [HttpGet]
        public IEnumerable<Log> GetLog()
        {
            IEnumerable<Log> result = _Ilog.GetAllAsync().Result.OrderByDescending(l => l.DateTime);
            return result;
        }

        // POST: api/Logs
        // [HttpPost]
        // [AllowAnonymous]
        // public async Task<ActionResult<Log>> PostLog([FromBody]LogViewModel logViewModel)
        // {
        //     Log log = new Log
        //     {
        //         Text = logViewModel.Text,
        //         DateTime = DateTime.Now
        //     };
        //     _Ilog.Add(log);
        //     await _Ilog.SaveAsync();

        //     return Ok();
        // }

        // DELETE: api/Logs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAllLog()
        {
            _Ilog.Clear();
            await _Ilog.SaveAsync();
            return Ok();
        }
    }
}
