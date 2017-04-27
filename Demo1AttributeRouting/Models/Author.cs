using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1AttributeRouting.Models
{
    public class Author
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
