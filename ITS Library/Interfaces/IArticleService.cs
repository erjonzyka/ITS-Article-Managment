using ITS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Interfaces
{
    public interface IArticleService
    {
        ICollection<Article> GetArticles(int ClientId);
        ICollection<Article> GetMyArticles(int UserId);
        bool CreateArticle(Article article, int CreatorId);
        int GetQuantity(int ArticleId);
        bool RemoveQuantity(int productId, int Quantity);
        bool AddQuantity(int productId, int Quantity);
        Article GetArticle(int productId);
    }
}
