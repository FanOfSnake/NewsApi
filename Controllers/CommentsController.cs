using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly NewsContext _context;

        public CommentsController(NewsContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        //{
        //    var comments = _context.Comments.Include(p => p.CurrNews);
        //    foreach (var obj0 in comments)
        //    {
        //        obj0.CurrNews.Comments = null;
        //        obj0.CurrNews.Categories = null;
        //    }

        //    return Ok(comments);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return Ok(await _context.Comments.Select(p => new { p.Id, p.WriterName, p.TimeWrite, p.Text, p.CurrNewsId }).AsNoTracking().ToListAsync());
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comments = await _context.Comments.Where(p => p.Id == id).Select(p=> new { p.Id, p.WriterName, p.TimeWrite, p.Text, p.CurrNewsId }).AsNoTracking().ToListAsync();

            if (comments.Count == 0)
                return NotFound();

            return Ok(comments);
        }

        /* PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutComment(int id, Comment comment)
        //{
        //    if (id != comment.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(comment).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CommentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}*/

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, [Bind("Id", "WriterName", "TimeWrite", "Text", "CurrNewsId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            var news = _context.News;

            if (!news.Where(p => p.Id == comment.CurrNewsId).Any())
                return BadRequest("The News You pointed doesn't exist!");

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment([Bind("WriterName", "Text", "CurrNewsId")]Comment comment)
        {
            comment.TimeWrite = DateTime.Now;
            comment.CurrNews = _context.News.Find(comment.CurrNewsId);
            if (comment.CurrNews == null)
                return BadRequest();
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, "Created comment ID is " + comment.Id);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
