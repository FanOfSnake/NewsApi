using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        // Новости данной катеории
        public List<News> News { get; set; } = new List<News>();
        // айдишники новостей для поста
        [NotMapped]
        public List<int> NewsId { get; set; } = new List<int>();
    }
}
