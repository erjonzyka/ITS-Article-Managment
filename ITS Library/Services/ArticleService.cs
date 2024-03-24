using ITS_Library.Interfaces;
using ITS_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Services
{
    public class ArticleService : IArticleService
    {
        public MyContext _context { get; }
        public ArticleService(MyContext context)
        {
            _context = context;
        }

        public ICollection<Article> GetArticles(int ClientId)
        {
            return _context.Articles.Include(e=> e.Creator).Where(e=> e.CreatorId != ClientId && e.Quantity > 0).ToList();
        }

        public ICollection<Article> GetMyArticles(int ClientId)
        {
            return _context.Articles.Include(e => e.Creator).Where(e => e.CreatorId == ClientId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateArticle(Article article, int CreatorId)
        {
            article.CreatorId = CreatorId;
            _context.Articles.Add(article);
            return Save();
        }

        public int GetQuantity(int productId)
        {
            return _context.Articles.FirstOrDefault(e => e.ArticleId == productId).Quantity;
        }

       public bool RemoveQuantity(int productId, int Quantity)
        {
            Article? article = GetArticle(productId);
            article.Quantity -= Quantity;
            return Save();
        }

        public bool AddQuantity(int productId, int Quantity)
        {
            Article? article = GetArticle(productId);
            article.Quantity += Quantity;
            return Save();
        }

        public Article? GetArticle(int productId)
        {
            return _context.Articles.FirstOrDefault(e => e.ArticleId == productId);
        }

    }
}
