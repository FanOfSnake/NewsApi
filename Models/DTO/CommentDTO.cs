using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Models.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string WriterName { get; set; }
        public string Text { get; set; }
        public DateTime TimeWrite { get; set; }
        public int CurrNewsId { get; set; }
    }
}
