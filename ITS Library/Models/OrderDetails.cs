using System;
using System.ComponentModel.DataAnnotations;

namespace ITS_Library.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int? OrderId { get; set; }
        public int? ArticleId { get; set; }
        public int Total { get; set; }
        public int Quantity { get; set; }
        public Article? article { get; set; }
    }
}
