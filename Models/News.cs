using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NewsApi.Models.DTO;

namespace NewsApi.Models
{
    
    public class News
    {
        public News ()
        {
            this.CategoriesId = new List<int>();
            this.CommentsId = new List<int>();
        }
        public News (NewsDTO news)
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
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<int> CategoriesId { get; set; }
        public List<int> CommentsId { get; set; }
    }
}
