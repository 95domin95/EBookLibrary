using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface IBooksManage
    {
        IEnumerable<Book> GetBooks(string title, int? ISBN, string author, int? pagesMin,
            int? pagesMax, string publisher, string category);
        IEnumerable<Category> GetAllCategories();
        Book GetById(int id);
        Category GetCategory(int id);
        Author GetAuthorById(int id);
        Publisher GetPublisher(string name);
        void Add(Book book);
        void DeleteById(int id);
        void UpdateById(int id, string newTitle, int? newISBN, string newAuthor,
            int? newPages, string newPublisher, string newCategory);
        int GetISBN(int id);
        string GetTitle(int id);
        int GetPages(int id);
        string GetPath(int id);
    }
}
