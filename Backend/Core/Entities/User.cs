using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstract;
using Microsoft.AspNetCore.Identity;


namespace Core.Entities
{
    public class User:IdentityUser
    {
        //public string Name { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }
        //public DateTime DateCreated { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Comment> Replies { get; set; }
        //[Key]
        //public Guid ID { get; set ; }
    }
}
