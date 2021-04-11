using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Models.DTO
{
    /// <summary>
    /// Category's entity
    /// </summary>
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
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description for the category
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// News that belongs to the category
        /// </summary>
        public List<int> NewsId { get; set; }
    }
}
