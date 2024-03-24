using ITS_Library.Interfaces;
using ITS_Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Services
{
    public class OrderService : IOrderService
    {
        private readonly MyContext _context;
        private readonly ICartService _cart;

        public OrderService(MyContext context, ICartService cart) {
            _cart = cart;
            _context = context;
        }

        public bool CompletePurchase(int userId)
        {
            Order order = new Order
            {
                UserId = userId,
            };
            _context.Add(order);
            _context.SaveChanges();
            ICollection<Cart> cartToPurchase = _cart.GetCarts(userId);
            foreach (Cart cart in cartToPurchase)
            {
                OrderDetails ord = new OrderDetails
                {
                    OrderId = order.OrderId,
                    ArticleId = cart.ArticleId,
                    Total = cart.Total,
                    Quantity = cart.Quantity,
                };
                _cart.RemoveCart(cart.CartId);
                _context.Add(ord);
            }
            return Save();
        }

        public ICollection<Order> GetMyOrders(int userId)
        {
            return _context.Orders.Include(e => e.OrderDetails).ThenInclude(e=> e.article).Where(e=> e.UserId == userId).ToList();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Include(e=> e.OrderDetails).ThenInclude(e=> e.article).FirstOrDefault(e=> e.OrderId == orderId);
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
