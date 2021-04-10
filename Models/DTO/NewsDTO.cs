using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Models.DTO
{
    public class NewsDTO
    {
        public NewsDTO()
        {
            this.CategoriesId = new List<int>();
            this.CommentsId = new List<int>();
        }
        public NewsDTO(News news)
        {
            this.Id = news.Id;
            this.Img = news.Img;
            this.Name = news.Name;
            this.ShortDesc = news.ShortDesc;
            this.TimePublication = news.TimePublication;
            this.Text = news.Text;
            this.CommentsId = news.CommentsId;
            this.CategoriesId = news.CategoriesId;
        }
        public int Id { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string Text { get; set; }
        public DateTime TimePublication { get; set; }
        public List<int> CategoriesId { get; set; }
        public List<int> CommentsId { get; set; }
    }
}
