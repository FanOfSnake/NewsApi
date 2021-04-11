using NewsApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsApi.Models
{
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
        public string WriterName { get; set; }
        public string Text { get; set; }
        public DateTime TimeWrite { get; set; }
        public News CurrNews { get; set; }
        public int CurrNewsId { get; set; }
    }
}
