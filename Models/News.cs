using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NewsApi.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string Text { get; set; }
        public DateTime TimePublication { get; set; }
        // категории данной новости
        public List<Category> Categories { get; set; } = new List<Category>();
        // комменты данной новости
        public List<Comment> Comments { get; set; } = new List<Comment>();

        // id комментариев и категорий для быстрого их создания
        [NotMapped]
        public int[] CategoriesId { get; set; }
        [NotMapped]
        public int[] CommentsId { get; set; }
    }
}
