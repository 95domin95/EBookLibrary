using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibraryData.Models
{
    public interface IBooksManage
    {
        IEnumerable<Book> GetBooks(string title, int? ISBN, string author, int? pagesMin,
            int? pagesMax, string publisher, string category);
        IEnumerable<Category> GetAllCategories();
        Book GetById(int id);
        Publisher GetPublisher(string name);
        Task<bool> Add(string Title, int? ISBN, int? Pages,
            string Author, string Publisher, string Category,
            IFormFile book, IFormFile bookCovering);
        bool DeleteById(int id);
        bool UpdateById(int? id, string newTitle, int? newISBN, string newAuthor,
            int? newPages, string newPublisher, string newCategory);
        int? GetISBN(int id);
        string GetTitle(int id);
        int? GetPages(int id);
        string GetPath(int id);
        Publisher AddPublisher(string name, string city);
    }
}
