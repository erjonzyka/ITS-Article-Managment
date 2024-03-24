using ITS_Library.Models;
using ITS_Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ITS_Library.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ITS_Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private MyContext _context;
        private readonly ILoginService _login;

        public LoginController(ILogger<LoginController> logger, MyContext context, ILoginService login)
        {
            _logger = logger;
            _context = context;
            _login = login;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                if (!_login.UserExists(user.LEmail)){
                    ModelState.AddModelError("LEmail", "Email invalid");
                    return View("Index");
                }
                User? userFromDb = _login.GetUser(user.LEmail);
                if(!_login.PasswordCheck(user,userFromDb.Password, user.LPassword))
                {
                    ModelState.AddModelError("LPassword", "Password invalid");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", userFromDb.id);
                return RedirectToAction("Index", "Article");
            }
            return View("Index");
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (!_login.SaveUser(user))
                {
                    ModelState.AddModelError("Email", "Error while saving!");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", user.id);
                return RedirectToAction("Index", "Article");
            }
            else
            {
                return View("Index");
            }
        }

        [SessionCheck]
        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}


public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
