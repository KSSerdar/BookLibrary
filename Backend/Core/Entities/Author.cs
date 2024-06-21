using Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Author : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
