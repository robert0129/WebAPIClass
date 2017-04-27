using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1AttributeRouting.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }

        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
