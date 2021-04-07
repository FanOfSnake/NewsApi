using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string WriterName { get; set; }
        public string Text { get; set; }
        public DateTime TimeWrite { get; set; }
        // новость, к которой относиться данный комментарий
        public News CurrNews { get; set; }
        // айдишник новости 
        public int CurrNewsId { get; set; }
    }
}
