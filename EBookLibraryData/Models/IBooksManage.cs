using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface IBooksManage
    {
        IEnumerable<Book> GetBooks(string title, int? ISBN, Author author, int? pagesMin,
            int? pagesMax, Publisher publisher, Category category);
        Book GetById(int id);
        Category GetCategory(int id);
        Author GetAuthor(int id);
        Publisher GetPublisher(int id);
        void Add(Book book);
        void DeleteById(int id);
        void UpdateById(int id, string newTitle, int? newISBN, IEnumerable<Author> newAuthor,
            int? newPages, Publisher newPublisher, IEnumerable<Category> newCategory);
        int GetISBN(int id);
        string GetTitle(int id);
        int GetPages(int id);
        string GetPath(int id);
    }
}
