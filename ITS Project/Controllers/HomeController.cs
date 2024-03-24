using ITS_Library.Interfaces;
using ITS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITS_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _article;
        private readonly IOrderService _order;

        public HomeController(ILogger<HomeController> logger, IArticleService article, IOrderService order)
        {
            _logger = logger;
            _article = article;
            _order = order;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index","Login");
        }

        [HttpGet("myprofile")]
        public IActionResult MyProfile()
        {
            ProductViewModel pm = new ProductViewModel()
            {
                Orders = _order.GetMyOrders((int)HttpContext.Session.GetInt32("UserId")),
                Articles = _article.GetMyArticles((int)HttpContext.Session.GetInt32("UserId")),

            };
            return View(pm);
        }


        [HttpGet("order/details/{id}")]
        public IActionResult OrderDetails(int id)
        {
            return View(_order.GetOrder(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
