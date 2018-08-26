using EBookLibraryData;
using EBookLibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookLibraryServices
{
    public class BooksManageService : IBooksManage
    {
        private Context _context;
        public BooksManageService(Context context)
        {
            _context = context;
        }
        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            _context.Books.Remove(GetById(id));
            _context.SaveChanges();
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors.Where(a => a.Name.Equals
            (a.BookAuthors.Where(ba => ba.BookId.Equals(id))
                .FirstOrDefault())).FirstOrDefault();
        }

        public IEnumerable<Book> GetBooks(string title="", int? ISBN = null,
            Author author = default(Author), int? pagesMin = null, int? pagesMax=null,
            Publisher publisher = default(Publisher), Category category = default(Category))
        {
            var resultBookAuthor = _context.BookAuthors.Take(100000).ToList();

            if(title != "")
            {
                resultBookAuthor = resultBookAuthor.Where(b => b.Book.Title.Contains(title)).ToList();
            }
            if(ISBN != null)
            {
                resultBookAuthor = resultBookAuthor.Where(b => b.Book.ISBN.Equals(ISBN)).ToList();
            }
            if (pagesMin != null)
            {
                resultBookAuthor = resultBookAuthor.Where(b => b.Book.Pages >= pagesMin).ToList();
            }
            if (pagesMax != null)
            {
                resultBookAuthor = resultBookAuthor.Where(b => b.Book.Pages <= pagesMax).ToList();
            }
            if(author != default(Author))
            {
                resultBookAuthor = resultBookAuthor.Where(b => b.Author.Equals(author)).ToList();
            }
            if (publisher != default(Publisher))
            {
                resultBookAuthor = resultBookAuthor.Where(b => b.Book.Publisher.Equals(publisher)).ToList();
            }

            var result = resultBookAuthor.Select(b => b.Book);

            //if(category != default(Category))
            //{
            //    result = resultCategory.Where()
            //}

            if(category != default(Category))
            {
                result = from book in result from bookAuthor in _context.BookAuthors where bookAuthor.Author.Equals(author) select book;
            }

            return result;
        }

        public Book GetById(int id)
        {
            return _context.Books.Where(b => b.BookId.Equals(id)).FirstOrDefault();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Name.Equals
            (c.BookCategory.Where(bc => bc.Book.BookId.Equals(id))
            .FirstOrDefault())).FirstOrDefault();
        }

        public int GetISBN(int id)
        {
            return _context.Books.Where(b => b.BookId.Equals(id)).Select(b => b.ISBN).FirstOrDefault();
        }

        public int GetPages(int id)
        {
            return _context.Books.Where(b => b.BookId.Equals(id)).Select(b => b.Pages).FirstOrDefault();
        }

        public string GetPath(int id)
        {
            return _context.Books.Where(b => b.BookId.Equals(id)).Select(b => b.Path).FirstOrDefault();
        }

        public Publisher GetPublisher(int id)
        {
            return _context.Publishers.Where(p => p.Name.Equals
            (p.Books.Where(b => b.BookId.Equals(id))
            .FirstOrDefault())).FirstOrDefault();
        }

        public string GetTitle(int id)
        {
            return _context.Books.Where(b => b.BookId.Equals(id)).Select(b => b.Title).FirstOrDefault();
        }

        public void UpdateById(int id, string newTitle="", int? newISBN=null,
            IEnumerable<Author> newAuthor=default(IEnumerable<Author>), int? newPages = null,
            Publisher newPublisher=default(Publisher), IEnumerable<Category> newCategory=default(IEnumerable<Category>))
        {
            var result = _context.Books.Where(b => b.BookId.Equals(id)).FirstOrDefault();

            var bookAuthor = _context.BookAuthors.Where(ba => ba.BookId.Equals(id)).ToList();

            var bookCategory = _context.BookCategories.Where(bc => bc.BookId.Equals(id)).ToList();
            
            if(result != default(Book))
            {
                result.BookId = id;
                result.Title = newTitle.Equals("") ? result.Title : newTitle;
                result.ISBN = newISBN == null ? result.ISBN : (int)newISBN;
                result.Pages = newPages == null ? result.Pages : (int)newPages;
                result.Publisher = newPublisher == default(Publisher) ? result.Publisher : newPublisher;
                result.BookAuthors = newAuthor == default(IEnumerable<BookAuthor>) ? result.BookAuthors : bookAuthor.ToList();
                result.BookCategory = bookCategory == default(IEnumerable<BookCategory>) ? result.BookCategory : bookCategory.ToList();
            }
   
            _context.SaveChanges();
        }
    }
}
