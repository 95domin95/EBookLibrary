using EBookLibraryData.Models.ViewModels.LibraryManage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibraryData.Models
{
    public interface IBooksManage
    {
        IEnumerable<Book> GetBooks(string title, int? ISBN, string[] author, int? pagesMin,
            int? pagesMax, string publisher, string category, int take=1000);
        IEnumerable<Category> GetAllCategories();
        Book GetById(int id);
        Book GetBookByPath(string path);
        Publisher GetPublisher(string name);
        Task<bool> Add(string title, int? ISBN, int? pages,
            string[] author, string publisher, string category,
            IFormFile book, IFormFile bookCovering, int copiesCount);
        IEnumerable<Copy> GetBookCopies(Book book);
        IEnumerable<Copy> GetAvailableBookCopies(Book book);
        IEnumerable<Copy> GetUserLoanCopies(ApplicationUser user);
        IEnumerable<Author> GetAllAuthors();
        bool DeleteById(int id);
        bool ChangeCopyRentedStatus(Copy copy);
        bool AddAuthor(string name);
        bool AddLoan(Loan loan);
        bool UpdateById(int? id, string newTitle, int? newISBN, string[] newAuthor,
            int? newPages, string newPublisher, string newCategory);
        bool DecrementBookCopy(Book book);
        bool IncrementBookCopy(Book book);
        bool RemoveUserLoan(ApplicationUser applicationUser, Book loanBook);
        int? GetISBN(int id);
        string GetTitle(int id);
        int? GetPages(int id);
        string GetPath(int id);
        Publisher AddPublisher(string name);
        Loan GetLoanByCopy(Copy copy);
        IEnumerable<Book> GetAllUserLoanedBooks(ApplicationUser user);
        IEnumerable<Book> GetMostPopularBooks(int booksCount = 10);
        IEnumerable<Book> GetMostRecentBooks(int booksCount = 10);
        IEnumerable<Book> GetMany(int take = 1000);
        IEnumerable<Book> LongestLoaned(int booksCount = 10);
        bool Modify(BooksManageViewModel model);
    }
}
