using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

//ПРОБЛЕМЫ: 1) пут метод не меняет сущностей, которые уже существуют
//        2) пОДТЯЖКА СУЩНОСТЕЙ ИЗ КТОНТЕКСТА - ПОЧЕМУ ТО НЕ РАБОТАЕТ - исправлено
            //3) Не возвращать некоторые поля наших сущностей - решено
            //4) Ошибка при неправлиьном выборе категории или коммента в посте - решено
            //5) реализовать пут метод - решено частично - почему категория не удаляется
            //6) можно не возвращать навиг свойства а только айдишники - через селект решается - остановился на  категориях 
            //7) Метод пут с биндингами и дин. добавлением данных 


namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsContext _context;

        public NewsController(NewsContext context)
        {
            _context = context;
        }

        /* GET: api/News
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<News>>> GetNews()
        //{
        //    var news = _context.News.Include(p => p.Categories).Include(p => p.Comments);
        //    foreach (var obj in news)
        //    {
        //        foreach (var obj1 in obj.Categories)
        //            obj1.News = null;
        //        foreach (var obj1 in obj.Comments)
        //            obj1.CurrNews = null;
        //    }
        //    return Ok(news);
        //}*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            return Ok(await _context.News.Include(p=>p.Categories).Include(p=>p.Comments).Select(p=>new { p.Id, p.Img, p.Name, p.ShortDesc, p.TimePublication, p.Text, p.Categories, p.Comments }).AsNoTracking().ToListAsync());
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(int id)
        {

            var news = await _context.News.Include(p => p.Categories).Include(p => p.Comments).Where(p => p.Id == id).Select(p => new { p.Id, p.Img, p.Name, p.ShortDesc, p.TimePublication, p.Text, p.Categories, p.Comments }).FirstAsync();
            if (news == null)
                return NotFound();
            return Ok(news);
        }

        // PUT: api/News/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNews(int id,[Bind("Img", "Name", "ShortDesc", "Text", "TimePublication", "CategoriesId", "CommentsId")] News news)
        {
            if (id != news.Id)
            {
                return BadRequest();
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

        /* POST: api/News
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<News>> PostNews(News news)
        //{
        //    _context.News.Add(news);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetNews", new { id = news.Id }, news);
        //}*/

        // метод с передачой айдишек категорий и комментов СДЕЛАЙ ПРОВЕРКУ АЙДИ КОММЕНТОВ И КАТЕГОРИЙ НА ИХ НАЛИЧИЕ
        [HttpPost]
        public async Task<ActionResult<News>> PostNews([Bind("Name", "Img", "ShortDesc", "Text", "CategoriesId", "CommentsId")] News news)
        {
            // добавляем неопределенные значения
            news.TimePublication = DateTime.Now;

            var categories = _context.Categories.Include(p => p.News).ToList();
            var comments = _context.Comments.Include(p => p.CurrNews).ToList();
            foreach (int i in news.CategoriesId)
            {
                var category = categories.Find(p => p.Id == i);
                if (category == null)
                    return BadRequest();
                news.Categories.Add(category);
            }
            foreach (int i in news.CommentsId)
            {
                var comment = comments.Find(p => p.Id == i);
                if(comment ==null)
                    return BadRequest();
                news.Comments.Add(comment);
            }

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNews), new { id = news.Id }, "Created news' ID is " + news.Id);
        }

        // DELETE: api/News/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.News.Include(d=>d.Comments).Where(p=>p.Id == id).FirstOrDefaultAsync();
            if (news == null)
            {
                return NotFound();
            }
            foreach (var i in news.Comments)
                _context.Comments.Remove(i);
            news.Comments.Clear();
            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
