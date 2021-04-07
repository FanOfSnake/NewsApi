using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Models
{
    public class CategoryNews
    {
        public int CategoriesId { get; set; }
        public int NewsId { get; set; }

        public virtual Category Categories { get; set; }
        public virtual News News { get; set; }
    }
}
