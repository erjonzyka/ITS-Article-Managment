using ITS_Library.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITS_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cart;
        private readonly IArticleService _article;

        public CartController(ICartService cart, IArticleService article)
        {
            _cart = cart;
            _article = article;
        }

        [HttpGet("article/cart/{articleId}")]
        public IActionResult AddToCart(int articleId)
        {
            if (_article.GetQuantity(articleId) < 1)
            {
                ModelState.AddModelError("Quantity", "Error adding to cart");
                return View("~/Views/Article/Index.cshtml", _article.GetArticles((int)HttpContext.Session.GetInt32("UserId")));
            }
            if (!_cart.AddToCart(articleId, (int)HttpContext.Session.GetInt32("UserId"), 1))
            {
                ModelState.AddModelError("Quantity", "Error adding to cart");
                return View("~/Views/Article/Index.cshtml", _article.GetArticles((int)HttpContext.Session.GetInt32("UserId")));
            }
            return RedirectToAction("Index", "Article");
        }

        [HttpGet("mycart")]
        public IActionResult MyCart()
        {
            return View(_cart.GetCarts((int)HttpContext.Session.GetInt32("UserId")));
        }

        [HttpPost("updatequantity/{id}")]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var qty = _cart.IsAvailable((int)HttpContext.Session.GetInt32("UserId"), id, quantity);
            _cart.UpdateCart(id, (int)HttpContext.Session.GetInt32("UserId"), qty);
            return RedirectToAction("MyCart");
        }

       
    }
}