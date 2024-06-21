using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class NewBook
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorsID { get; set; }
    }
}
