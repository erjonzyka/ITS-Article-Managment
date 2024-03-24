using ITS_Library.Interfaces;
using ITS_Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Services
{
    public class CartService : ICartService
    {
        private readonly MyContext _context;
        private readonly ILogger<ArticleService> _logger;

        public IArticleService _article { get; }

        public CartService(MyContext context,IArticleService article, ILogger<ArticleService> logger) {
            _context = context;
            _article = article;
            _logger = logger;
        }

        public bool IsInCart(int productId, int userId)
        {
            return _context.Carts.Any(c => c.ArticleId == productId && c.UserId == userId && c.Finished == false);
        }

        public Cart GetCart(int productId, int userId)
        {
            return _context.Carts.FirstOrDefault(c => c.ArticleId == productId && c.UserId == userId);
        }

        public bool AddToCart(int productId, int userId, int Quantity)
        {
            Console.WriteLine("qty comes" + Quantity);

            if (IsInCart(productId, userId))
            {
                Cart cart = GetCart(productId, userId);
                int actualQty = cart.Quantity;
                int qtyToUpd = Quantity + actualQty;
                return UpdateCart(productId, userId, qtyToUpd);
            }
            else
            {
                Cart newCart = new Cart
                {
                    UserId = userId,
                    ArticleId = productId,
                    Quantity = Quantity
                };
                newCart.Total = CalculateTotal(productId, Quantity);

                _context.Add(newCart);
                bool updateQuantity = _article.RemoveQuantity(productId, Quantity);

                return Save() && updateQuantity;
            }
        }

        public bool UpdateCart(int productId, int userId, int quantity)
        {
            Cart? cart = GetCart(productId, userId);

            
                int prevQuantity = cart.Quantity;
            _logger.LogWarning($"prev qty {prevQuantity}");
                int quantityChange = quantity - prevQuantity;

                cart.Quantity = quantity;
            _logger.LogWarning($"cart qty {cart.Quantity}");
            if (quantityChange > 0)
                {
                    bool updateQuantity = _article.RemoveQuantity(productId, quantityChange);
                    
                }
                else if (quantityChange < 0)
                {
                    bool updateQuantity = _article.AddQuantity(productId, 0 - quantityChange);
                }

                cart.Total = CalculateTotal(productId, quantity);


                return Save();
            }

  
        public bool RemoveCart(int cartId)
        {
            Cart? cart = _context.Carts.FirstOrDefault(e => e.CartId == cartId);
            _context.Remove(cart);
            return Save();
        }



        private int CalculateTotal(int productId, int qty)
        {
            return _article.GetArticle(productId).Price * qty;
        }

        public ICollection<Cart> GetCarts(int userId)
        {
            return _context.Carts.Include(e=> e.Article).Where(e=> e.UserId == userId && e.Finished == false).ToList();
        }

        public ICollection<Cart> GetMyOrders(int userId)
        {
            return _context.Carts.Include(e => e.Article).Where(e => e.UserId == userId && e.Finished == true).ToList();
        }



        public int IsAvailable(int userId, int productId, int qty)
        {
            var reqCart = GetCart(productId, userId);
            var reqArticle = _article.GetArticle(productId);
            if(reqCart.Quantity + reqArticle.Quantity < qty)
            {
                return reqCart.Quantity + reqArticle.Quantity;
            }
            return qty;
        }


        

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
