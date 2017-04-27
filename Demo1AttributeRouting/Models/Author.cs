using System.ComponentModel.DataAnnotations;

namespace Demo1AttributeRouting.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
