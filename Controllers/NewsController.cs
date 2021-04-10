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
    public class NewsController : ControllerBase
    {
        private readonly NewsContext _context;

        public NewsController(NewsContext context)
        {
            _context = context;
        }

        ///<summary>Returning all the news</summary>
        ///<returns>All the news</returns>
        ///<response code="200">Returning all the news</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetNews()
        {
            var news = await _context.News.Include(p => p.Categories).Include(p => p.Comments).ToListAsync();
            List<NewsDTO> newsDTO = new List<NewsDTO>();
            foreach (var OneNews in news)
            {
                foreach (var category in OneNews.Categories)
                    OneNews.CategoriesId.Add(category.Id);
                foreach (var comment in OneNews.Comments)
                    OneNews.CommentsId.Add(comment.Id);
                newsDTO.Add(new NewsDTO(OneNews));
            }
            return Ok(newsDTO);
        }

        ///<summary>Returning the news with the unique ID</summary>
        ///<param name="id"></param>
        ///<response code="404">There is no news with pointed ID!</response>
        ///<response code="200">Returning the news with the unique ID</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<NewsDTO>> GetNews(int id)
        { 
            var news = _context.News.Include(p => p.Categories).Include(p => p.Comments).Where(p => p.Id == id);
            if (!news.Any())
                return NotFound("There is no news with pointed ID!");

            foreach (var category in news.First().Categories)
                news.First().CategoriesId.Add(category.Id);
            foreach (var comment in news.First().Comments)
                news.First().CommentsId.Add(comment.Id);

            return Ok(new NewsDTO(await news.FirstAsync()));
        }

        ///<summary>Updating the News with unique ID</summary>
        ///<response code="400">The entity you pointed doesn't exist!</response>
        ///<response code="204">The news was successfully changed!</response>
        ///<remarks>
        ///
        ///Sample request:
        ///
        ///    {
        ///         "id": 1,
        ///         "Name": "The changed Name!",
        ///         "Img": "SomeURL for img",
        ///         "ShortDesc": "Some changed descryption.",
        ///         "Text": "Some changed text.",
        ///         "TimePublication": "12.31.1999",
        ///         "CategoriesId": [],
        ///         "CommentsId": []
        ///    }
        /// </remarks>
        /// 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutNews(int id,NewsDTO news)
        {
            if (id != news.Id)
            {
                return BadRequest("There is no news with ID You pointed");
            }

            var categories = _context.Categories;
            //проверка сущ-ия новостей  
            foreach (var i in news.CategoriesId)
                if (!categories.Where(p => p.Id == i).Any())
                    return BadRequest("The Category you pointed doesn't exist!");

            var comments = _context.Comments;
            //проверка сущ-ия новостей  
            foreach (var i in news.CommentsId)
                if (!comments.Where(p => p.Id == i).Any())
                    return BadRequest("The Comment you pointed doesn't exist!");

            var OldNews = _context.News.Include(p => p.Categories).Include(p=>p.Comments).Where(p => p.Id == news.Id).FirstOrDefault();
            OldNews.Categories.Clear();
            OldNews.Comments.Clear();

            foreach (var i in news.CategoriesId)
                OldNews.Categories.Add(categories.Find(i));
            foreach (var i in news.CommentsId)
                OldNews.Comments.Add(comments.Find(i));

            OldNews.Img = news.Img;
            OldNews.Name = news.Name;
            OldNews.ShortDesc = news.ShortDesc;
            OldNews.Text = news.Text;
            OldNews.TimePublication = news.TimePublication;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        ///<summary>Creating a new News entity</summary>
        ///<response code="400">There is no entities with pointed ID!</response>
        ///<response code="201">Returning the news which was created</response>
        ///<remarks>
        ///Sample request:
        ///
        ///    {
        ///         "Name": "The created Name!",
        ///         "Img": "SomeURL for img",
        ///         "ShortDesc": "Some created descryption.",
        ///         "Text": "Some created text.",
        ///         "TimePublication": "12.31.1999",
        ///         "CategoriesId": [],
        ///         "CommentsId": []
        ///    }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<News>> PostNews(NewsDTO newsDTO)
        {
            News news = new News(newsDTO);
            // добавляем неопределенные значения
            news.TimePublication = DateTime.Now;

            var categories = _context.Categories.Include(p => p.News).ToList();
            var comments = _context.Comments.Include(p => p.CurrNews).ToList();
            foreach (int i in news.CategoriesId)
            {
                var category = categories.Find(p => p.Id == i);
                if (category == null)
                    return BadRequest("The categories you pointed doesn't exist!");
                news.Categories.Add(category);
            }
            foreach (int i in news.CommentsId)
            {
                var comment = comments.Find(p => p.Id == i);
                if(comment ==null)
                    return BadRequest("The comments you pointed doesn't exist!");
                news.Comments.Add(comment);
            }

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNews), new { id = news.Id }, new NewsDTO(news));
        }

        ///<summary>Deleting the News with unique ID</summary>
        ///<param name="id"></param>
        ///<returns>NoContent in case of success</returns>
        ///<response code="404">There is no news with pointed ID!</response>
        ///<response code="204">Returning NoContent in case of success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = _context.News.Include(d=>d.Comments).Where(p=>p.Id == id);
            if (!await news.AnyAsync())
                return NotFound("There is no news with pointed ID!");
            foreach (var i in news.First().Comments)
                _context.Comments.Remove(i);
            news.First().Comments.Clear();
            _context.News.Remove(news.First());
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
