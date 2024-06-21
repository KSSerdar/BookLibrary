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
    public class Comment : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [ForeignKey("BookID")]
        public Book Book { get; set; }
        public Guid BookID { get; set; }
        public string  Description { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        public string UserID { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
