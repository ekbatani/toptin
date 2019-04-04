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
    public class CommentsController : ControllerBase
    {
        private readonly IComment _IComment;

        public CommentsController(IComment IComment)
        {
            _IComment = IComment;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComment()
        {
            return await _IComment.GetAllAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _IComment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _IComment.Update(comment);

            try
            {
                await _IComment.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _IComment.Add(comment);
            await _IComment.SaveAsync();

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _IComment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _IComment.Remove(comment);
            await _IComment.SaveAsync();

            return comment;
        }

        private bool CommentExists(int id)
        {
            return _IComment.Any(id).Result;
        }
    }
}
