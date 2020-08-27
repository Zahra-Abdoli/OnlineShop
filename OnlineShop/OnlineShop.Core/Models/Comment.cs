using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
    public class Comment:BaseEntity
    {
        public int CommentId { get; set; }
        public string ProductId { get; set; }

        public string CommentDescritption { get; set; }

        public int Rating { get; set; }

        public DateTime CommentedOn { get; set; }
    }
}
