using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DefaultValue("Some cool name!")]
        public string Name { get; set; }
        [DefaultValue("Some cool descrypton!")]
        public string Desc { get; set; }
        // Новости данной катеории
        public List<News> News { get; set; } = new List<News>();
        // айдишники новостей для поста
        [NotMapped]
        public List<int> NewsId { get; set; } = new List<int>();
    }
}
