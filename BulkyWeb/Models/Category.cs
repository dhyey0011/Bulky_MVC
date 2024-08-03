using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        //[Key] //you can use this or mention the model name in the variable name
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage = "It only accepts number from 1-100")]
        public int DisplayOrder  { get; set; }
    }
}
