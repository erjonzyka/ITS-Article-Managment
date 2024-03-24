using ITS_Library.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITS_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _order;

        public OrderController(IOrderService order) {
            _order = order;
        }

        [HttpGet("complete/purchase")]
        public IActionResult CompletePurchase()
        {
            _order.CompletePurchase((int)HttpContext.Session.GetInt32("UserId"));
            return RedirectToAction("Index", "Article");
        }
    }
}
