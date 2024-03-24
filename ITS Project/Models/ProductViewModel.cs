using ITS_Library.Models;

namespace ITS_Project.Models
{
    public class ProductViewModel
    {
       public ICollection <Article>? Articles { get; set; }
       public ICollection<Order>? Orders { get; set; }
    }
}
