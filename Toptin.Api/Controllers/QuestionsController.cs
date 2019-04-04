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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestion _IQuestion;

        public QuestionsController(IQuestion IQuestion)
        {
            _IQuestion = IQuestion;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<IEnumerable<Question>> GetQuestion()
        {
            return await _IQuestion.GetAllAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _IQuestion.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.QuestionId)
            {
                return BadRequest();
            }

            _IQuestion.Update(question);

            try
            {
                await _IQuestion.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            _IQuestion.Add(question);
            await _IQuestion.SaveAsync();

            return CreatedAtAction("GetQuestion", new { id = question.QuestionId }, question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            var question = await _IQuestion.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _IQuestion.Remove(question);
            await _IQuestion.SaveAsync();

            return question;
        }

        private bool QuestionExists(int id)
        {
            return _IQuestion.Any(id).Result;
        }
    }
}
