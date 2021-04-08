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
        public int Id { get; set; }
        [Required]
        [DefaultValue("Some cool Writer")]
        public string WriterName { get; set; }
        [Required]
        [DefaultValue("Some cool comment text!")]
        public string Text { get; set; }
        [DefaultValue("01.01.2000")]
        public DateTime TimeWrite { get; set; }
        // новость, к которой относиться данный комментарий
        public News CurrNews { get; set; }
        [Required]
        // айдишник новости 
        public int CurrNewsId { get; set; }
    }
}
