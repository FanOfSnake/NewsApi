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
        public List<News> News { get; set; } = new List<News>();
        // айдишники новостей для поста
        public List<int> NewsId { get; set; } = new List<int>();
    }
}
