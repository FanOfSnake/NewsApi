using NewsApi.Models.DTO;
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
        public Category()
        {
            News = new List<News>();
            NewsId = new List<int>();
        }
        public Category (CategoryDTO category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Desc = category.Desc;
            this.NewsId = category.NewsId;
            News = new List<News>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public List<News> News { get; set; }
        public List<int> NewsId { get; set; }
    }
}
