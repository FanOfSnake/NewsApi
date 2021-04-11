using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Models.DTO
{
    /// <summary>
    /// Comment's entity
    /// </summary>
    public class CommentDTO
    {
        public CommentDTO() { }
        public CommentDTO(Comment comment)
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
        /// <summary>
        /// The target news of the comment
        /// </summary>
        public int CurrNewsId { get; set; }
    }
}
