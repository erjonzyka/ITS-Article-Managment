using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace ITS_Library.Models
    {
        public class Order
        {
            [Key]
            public int OrderId { get; set; }
            public int? UserId { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }

        public User? User { get; set; }
        }
    }


