using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Models.DTO
{
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
        public string WriterName { get; set; }
        public string Text { get; set; }
        public DateTime TimeWrite { get; set; }
        public int CurrNewsId { get; set; }
    }
}
