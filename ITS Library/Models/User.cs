using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITS_Library.Models
{
    public class User
    {

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(2, ErrorMessage = "Last Name must be at leastA 2 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
        [UniqueEmail]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Passwords must match!")]
        public string Pwconfirm { get; set; }

        public int Points { get; set; } = 0;

        public int Role { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Article>? MyArticles { get; set; }
        public ICollection<Order>? MyOrders{ get; set; }

    }


    }

