using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value cannot be smaller than 1")]
        public int Quantity { get; set; }
        public bool Finished { get; set; } = false;

        public int Total { get; set; } = 0;
        public int? ArticleId { get; set; }

        public int? UserId { get; set; }

        public Article? Article { get; set; }
        public User? User { get; set; }
    }
}
