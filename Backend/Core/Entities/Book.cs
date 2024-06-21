using Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Book : IEntity
    {
        [Key]
        public Guid ID { get; set ; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureURL { get; set; }
        public DateTime PublishDate { get; set; }

        public ICollection<Comment> Replies { get; set; }
        [ForeignKey("AuthorID")]
        public Author? Author { get; set; }
        public Guid AuthorID { get; set; }

    }
}
