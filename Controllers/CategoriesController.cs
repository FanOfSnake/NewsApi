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
    public class CategoriesController : ControllerBase
    {
        private readonly NewsContext _context;

        public CategoriesController(NewsContext context)
        {
            _context = context;
        }

        ///<summary>Returning all the categories</summary>
        ///<returns>All the categories</returns>
        ///<response code="200">Returning all the categories</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = _context.Categories.Include(p => p.News);
            foreach (var category in categories)
                foreach (var news in category.News)
                    category.NewsId.Add(news.Id);
            return Ok(await categories
                .Select(p => new CategoryDTO{
                    Id = p.Id,
                    Name = p.Name,
                    Desc = p.Desc,
                    NewsId = p.NewsId
                })
                .ToListAsync());
        }

        ///<summary>Returning the category with the unique ID</summary>
        ///<param name="id"></param>
        ///<response code="404">There is no category with pointed ID!</response>
        ///<response code="200">Returning the category with the unique ID</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {                       
            var category = _context.Categories.Include(p => p.News).Where(p=>p.Id == id);
            if (!category.Any())
                return NotFound("There is no category with pointed ID!");
            foreach (var i in category.First().News)
                category.First().NewsId.Add(i.Id);

            return Ok(await category
                .Select(p => new CategoryDTO{
                    Id = p.Id,
                    Name = p.Name,
                    Desc = p.Desc,
                    NewsId = p.NewsId 
                })
                .ToListAsync());
        }

        ///<summary>Updating the category with unique ID</summary>
        ///<response code="400">The entity you pointed doesn't exist!</response>
        ///<response code="204">The category was successfully changed!</response>
        ///<remarks>
        ///
        ///Sample request:
        ///
        ///    {
        ///         "id": 1,
        ///         "Name": "The changed Name!",
        ///         "Desc": "The changed description!",
        ///         "NewsId": []
        ///    }
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO category)
        {
            if (id != category.Id)
            {
                return BadRequest("The category you pointed doesn't exist!");
            }

            var news = _context.News;
            //проверка сущ-ия новостей  
            foreach (var i in category.NewsId)
                if (!news.Where(p => p.Id == i).Any())
                    return BadRequest("The News you pointed doesn't exist!");

            var OldCategory = _context.Categories.Include(p => p.News).Where(p => p.Id == category.Id).FirstOrDefault();
            OldCategory.News.Clear();

            foreach (var i in category.NewsId)
                OldCategory.News.Add(news.Find(i));

            OldCategory.Name = category.Name;
            OldCategory.Desc = category.Desc;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        ///<summary>Creating a new category's entity</summary>
        ///<response code="400">There is no news with pointed ID!</response>
        ///<response code="201">Returning the category which was created</response>
        ///<remarks>
        ///Sample request:
        ///
        ///    {
        ///         "Name": "The created Name!",
        ///         "Desc": "The created description!",
        ///         "NewsId": []
        ///    }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> PostCategory(CategoryDTO categoryDTO)
        {
            var news = await _context.News.Include(p => p.Categories).ToListAsync();
            Category category = new Category(categoryDTO);
            foreach (int i in category.NewsId)
            {
                var OneNews = news.Where(p=>p.Id == i);
                if (!OneNews.Any())
                    return BadRequest("There is no news with pointed ID!");
                category.News.Add(OneNews.FirstOrDefault());
            }
            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, new CategoryDTO(category));
        }

        ///<summary>Deleting the category with unique ID</summary>
        ///<param name="id"></param>
        ///<response code="404">There is no category with pointed ID!</response>
        ///<response code="204">Returning NoContent in case of success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = _context.Categories.Where(p=>p.Id == id);
            if (!await category.AnyAsync())
            {
                return NotFound("There is no category with pointed ID!");
            }

            _context.Categories.Remove(category.First());
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
