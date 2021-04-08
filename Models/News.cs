using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Models
{
    public class News
    {
        public int Id { get; set; }
        [DefaultValue("SomeUrl")]
        public string Img { get; set; }
        [Required]
        [DefaultValue("Some cool name!")]
        public string Name { get; set; }
        [DefaultValue("Some cool descrypton!")]
        public string ShortDesc { get; set; }
        [Required]
        [DefaultValue("Some cool text!")]
        public string Text { get; set; }
        [DefaultValue("01.01.2000")]
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
