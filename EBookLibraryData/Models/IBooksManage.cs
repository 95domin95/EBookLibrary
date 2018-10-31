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
            IFormFile book, IFormFile bookCovering, int copiesCount);
        IEnumerable<Copy> GetBookCopies(Book book);
        IEnumerable<Copy> GetAvailableBookCopies(Book book);
        IEnumerable<Copy> GetUserLoanCopies(ApplicationUser user);
        bool DeleteById(int id);
        bool AddLoan(Loan loan);
        bool UpdateById(int? id, string newTitle, int? newISBN, string newAuthor,
            int? newPages, string newPublisher, string newCategory);
        bool DecrementBookCopy(Book book);
        bool IncrementBookCopy(Book book);
        bool RemoveUserLoan(ApplicationUser applicationUser, Book loanBook);
        int? GetISBN(int id);
        string GetTitle(int id);
        int? GetPages(int id);
        string GetPath(int id);
        Publisher AddPublisher(string name, string city);
        Loan GetLoanByCopy(Copy copy);
    }
}
