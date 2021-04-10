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
    /// Comment's entity
    /// </summary>
    public class Comment
    {
        public Comment() { }
        public Comment(CommentDTO comment)
        {
            this.Id = comment.Id;
            this.WriterName = comment.WriterName;
            this.Text = comment.Text;
            this.TimeWrite = comment.TimeWrite;
            this.CurrNewsId = comment.CurrNewsId;
        }
        public int Id { get; set; }
        /// <summary>
        /// User that wrote the comment
        /// </summary>
        public string WriterName { get; set; }
        /// <summary>
        /// The text of the comment
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The time comment was published
        /// </summary>
        public DateTime TimeWrite { get; set; }
        // новость, к которой относиться данный комментарий
        /// <summary>
        /// The target news of the comment
        /// </summary>
        public News CurrNews { get; set; }
        // айдишник новости 
        public int CurrNewsId { get; set; }
    }
}
