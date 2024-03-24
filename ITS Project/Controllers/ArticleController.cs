using ITS_Library.Interfaces;
using ITS_Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ITS_Project.Controllers
{
    public class ArticleController : Controller
    {
        public IArticleService _article { get; }
        public ArticleController(IArticleService article) {
            _article = article;
        }


        [SessionCheck]
        public IActionResult Index()
        {
            ICollection<Article> Articles = _article.GetArticles((int)HttpContext.Session.GetInt32("UserId"));
            return View(Articles);
        }

        [SessionCheck]
        [HttpGet("create/article")]
        public IActionResult CreateArticle()
        {
            return View();
        }

        [SessionCheck]
        [HttpPost]
        public IActionResult AddArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateArticle");
            }
            if (!_article.CreateArticle(article, (int)HttpContext.Session.GetInt32("UserId")))
            {
                ModelState.AddModelError("Name", "Something went wrong while saving");
                return View("CreateArticle");
            }
            return RedirectToAction("Index");
        }
    }
}
