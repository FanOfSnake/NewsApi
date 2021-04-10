using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Models
{
    /// <summary>
    /// News' entity
    /// </summary>
    public class News
    {
        public int Id { get; set; }
        /// <summary>
        /// Url for the New's img
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// The name of the news
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The short description for the news
        /// </summary>
        public string ShortDesc { get; set; }
        /// <summary>
        /// The text of the news
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The publication time of the news
        /// </summary>
        public DateTime TimePublication { get; set; }
        /// <summary>
        /// The new's categories
        /// </summary>
        public List<Category> Categories { get; set; } = new List<Category>();
        /// <summary>
        /// The new's comments
        /// </summary>
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public int[] CategoriesId { get; set; }
        public int[] CommentsId { get; set; }
    }
}
