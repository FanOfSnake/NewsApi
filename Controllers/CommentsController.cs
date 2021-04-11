using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApi.Models;
using NewsApi.Models.DTO;

namespace NewsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly NewsContext _context;

        public CommentsController(NewsContext context)
        {
            _context = context;
        }

        ///<summary>Returning all the comments</summary>
        ///<returns>All the comments</returns>
        ///<response code="200">Returning all the comments</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
        {
            return Ok(
                await _context
                .Comments
                .Select(p => new CommentDTO { 
                    Id =p.Id, 
                    WriterName = p.WriterName, 
                    TimeWrite = p.TimeWrite, 
                    Text = p.Text, 
                    CurrNewsId = p.CurrNewsId 
                }).AsNoTracking()
                .ToListAsync()
                );
        }

        ///<summary>Returning the comment with the unique ID</summary>
        ///<param name="id"></param>
        ///<response code="404">There is no comment with pointed ID!</response>
        ///<response code="200">Returning the comment with the unique ID</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CommentDTO>> GetComment(int id)
        {
            var comments = await _context
                .Comments
                .Where(p=>p.Id == id)
                .Select(p => new CommentDTO()
                {
                    Id = p.Id,
                    WriterName = p.WriterName,
                    Text = p.Text,
                    TimeWrite = p.TimeWrite,
                    CurrNewsId = p.CurrNewsId
                })
                .AsNoTracking()
                .ToListAsync();

            if (comments.Count == 0)
                return NotFound("There is no comment with pointed ID!");

            return Ok(comments);
        }

        ///<summary>Updating the comment with unique ID</summary>
        ///<response code="400">The entity you pointed doesn't exist!</response>
        ///<response code="204">The comment was successfully changed!</response>
        ///<remarks>
        ///
        ///Sample request:
        ///
        ///    {
        ///         "id": 1,
        ///         "WriterName": "The changed Name!",
        ///         "TimeWrite": "12.31.1999",
        ///         "Text": "Some changed text.",
        ///         "CurrNewsId": 1
        ///    }
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutComment(int id, CommentDTO commentDTO)
        {
            Comment comment = new Comment(commentDTO);
            if (id != comment.Id)
            {
                return BadRequest("The comment you pointed doesn't exist!");
            }

            var news = _context.News;

            if (!news.Where(p => p.Id == comment.CurrNewsId).Any())
                return BadRequest("The News You pointed doesn't exist!");

            _context.Entry(comment).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        ///<summary>Creating a new comment's entity</summary>
        ///<response code="400">There is no entities with pointed ID!</response>
        ///<response code="201">Returning the comment which was created</response>
        ///<remarks>
        ///Sample request:
        ///
        ///    {
        ///         "WriterName": "The created Name",
        ///         "TimeWrite": "12.31.1999",
        ///         "Text": "Some created text!",
        ///         "CurrNewsId": 1
        ///    }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDTO>> PostComment(CommentDTO commentDTO)
        {
            Comment comment = new Comment(commentDTO);

            comment.TimeWrite = DateTime.Now;
            comment.CurrNews = await _context.News.FindAsync(comment.CurrNewsId);
            if (comment.CurrNews == null)
                return BadRequest("There is no news with pointed ID!");
            _context.Comments.Add(comment);
            _context.SaveChanges();


            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, new CommentDTO(comment));
        }

        ///<summary>Deleting the comment with unique ID</summary>
        ///<param name="id"></param>
        ///<response code="404">There is no comment with pointed ID!</response>
        ///<response code="204">Returning NoContent in case of success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = _context.Comments.Where(p=>p.Id == id);
            if (!await comment.AnyAsync())
            {
                return NotFound("There is no comment with pointed ID!");
            }

            _context.Comments.Remove(comment.First());
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
