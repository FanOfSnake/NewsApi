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
    //1) ПРРОБЛЕМА С ПУТОМ - ДУБЛИКАЦИЯ КЛЮЧЕЙ В ДОП ТАБЛИЦЕ - ПОПРОУЙ ЕЕ ПРОСТО ОЧИЩАТЬ ОТ ДАННФХ С ОПРЕДЕЛЕННЫМ АЙДИ
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NewsContext _context;

        public CategoriesController(NewsContext context)
        {
            _context = context;
        }

        /* GET: api/Categories
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        //{
        //    var categories = _context.Categories.Include(p => p.News);
        //    foreach(var obj0 in categories)
        //        foreach(var obj1 in obj0.News)
        //        {
        //            obj1.Categories = null;
        //            obj1.Comments = null;
        //        }
        //    return Ok(categories);
        //}*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = _context.Categories.Include(p => p.News);
            foreach (var category in categories)
                foreach (var news in category.News)
                    category.NewsId.Add(news.Id);
            return Ok(await categories.Select(p => new { p.Id, p.Name, p.Desc, p.NewsId }).ToListAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = _context.Categories.Include(p => p.News).Where(p=>p.Id == id);
            if (!category.Any())
                return NotFound();
            foreach (var i in category.First().News)
                category.First().NewsId.Add(i.Id);

            return Ok(await category.Select(p => new { p.Id, p.Name, p.Desc, p.NewsId }).ToListAsync());
        }

        /* PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCategory(int id, Category category)
        //{
        //    if (id != category.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(category).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryExists(id))
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
        public async Task<IActionResult> PutCategory(int id, [Bind("Id", "Name", "Desc", "NewsId")]Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var news = _context.News;
            //проверка сущ-ия новостей  
            foreach (var i in category.NewsId)
                if (!news.Where(p => p.Id == i).Any())
                    return BadRequest("The News you pointed doesn't exist!");

            var OldCategory = _context.Categories.Include(p=>p.News).Where(p=>p.Id == category.Id).FirstOrDefault();
            OldCategory.News.Clear();

            foreach (var i in category.NewsId)
                OldCategory.News.Add(news.Find(i));

            OldCategory.Name = category.Name;
            OldCategory.Desc = category.Desc;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory([Bind("Name", "Decs", "NewsId")]Category category)
        {
            var news = _context.News.Include(p => p.Categories).ToList();
            foreach (int i in category.NewsId)
            {
                var OneNews = news.Find(p => p.Id == i);
                if (OneNews == null)
                    return BadRequest();
                category.News.Add(OneNews);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, "Created category's ID is " + category.Id);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
