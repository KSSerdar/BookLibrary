using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class AddCommentModel
    {
        public string Comment { get; set; }
        public Guid BookID { get; set; }
    }
}
