using NewsApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsApi.Models
{
    /// <summary>
    /// Category's entity
    /// </summary>
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
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description for the category
        /// </summary>
        public string Desc { get; set; }
        // Новости данной катеории
        /// <summary>
        /// News that belongs to the category
        /// </summary>
        public List<News> News { get; set; }
        // айдишники новостей для поста
        public List<int> NewsId { get; set; }
    }
}
