using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Models.DTO
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {
        }
        public CategoryDTO(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Desc = category.Desc;
            this.NewsId = category.NewsId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public List<int> NewsId { get; set; }
    }
}
