using ITS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Interfaces
{
    public interface ICartService
    {
        bool AddToCart(int productId, int userId, int Quantity);
        bool IsInCart(int productId, int userId);
        bool UpdateCart(int productId, int userId, int quantity);
        Cart GetCart(int productId, int userId);
        ICollection<Cart> GetCarts(int userId);
        ICollection<Cart> GetMyOrders(int userId);
        int IsAvailable(int userId, int productId, int qty);
        bool RemoveCart(int CartId);
        

    }
}
