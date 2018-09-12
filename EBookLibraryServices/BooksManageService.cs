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
        public void Add(string title, int? ISBN, int? pages,
            string author, string publisher, string category)
        {
            if(title != null && !title.Equals(String.Empty))
            {
                var bookAuthor = _context.BookAuthors.Where(ba => ba.Author.Name.Equals(author)).FirstOrDefault();
                var bookPublisher = _context.Publishers.Where(p => p.Name.Equals(publisher)).FirstOrDefault();
                var bookCategory = _context.BookCategories.Where(c => c.Category.Name.Equals(category)).FirstOrDefault();

                _context.Books.Add(new Book
                {
                    Title = title,
                    BookAuthors = new List<BookAuthor>() { bookAuthor },
                    Publisher = bookPublisher,
                    ISBN = ISBN,
                    Pages = pages,
                    BookCategory = new List<BookCategory>() { bookCategory }

                });
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            _context.Books.Remove(GetById(id));
            _context.SaveChanges();
        }

        public Author GetAuthorById(int id)
        {
            return _context.Authors.Where(a => a.Name.Equals
            (a.BookAuthors.Where(ba => ba.BookId.Equals(id))
                .FirstOrDefault())).FirstOrDefault();
        }

        public Author GetAuthor(string name)
        {
            return (from bookAuthor in _context.BookAuthors where 
                    bookAuthor.Author.Name.Equals(name) select bookAuthor.Author).FirstOrDefault();
        }

        public Publisher GetPublisher(string name)
        {
            return _context.Publishers.Where(p => p.Name.Equals(name)).FirstOrDefault();
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<Book> GetBooks(string title="", int? ISBN = null,
            string author = "", int? pagesMin = null, int? pagesMax=null,
            string publisher = "", string category = "")
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
            if(author != "")
            {
                var authorResult = _context.Authors.Where(a => a.Name.Equals(author)).FirstOrDefault();
                resultBookAuthor = resultBookAuthor.Where(b => b.Author.Equals(author)).ToList();
            }
            if (publisher != "")
            {
                var publisherResult = _context.Publishers.Where(p => p.Name.Equals(publisher)).FirstOrDefault();
                resultBookAuthor = resultBookAuthor.Where(b => b.Book.Publisher.Equals(publisher)).ToList();
            }

            var result = resultBookAuthor.Select(b => b.Book);

            if(category != "")
            {
                result = from book in result from bookCategory in _context.BookCategories where bookCategory.Category.Equals(category) select book;
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

        public int? GetISBN(int id)
        {
            return _context.Books.Where(b => b.BookId.Equals(id)).Select(b => b.ISBN).FirstOrDefault();
        }

        public int? GetPages(int id)
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

        public void UpdateById(int? id, string newTitle="", int? newISBN=null,
            string newAuthor="", int? newPages = null,
            string newPublisher="", string newCategory="")
        {
            //wypierdala sie tutaj bo GetBooks chyba nulla zwraca

            IEnumerable<Book> foundBooks = null;

            int? bookId = id;

            if (id == null)
            {
                foundBooks = GetBooks(newTitle, newISBN, newAuthor, null,
                null, newPublisher, newCategory);

                //ogarnąć tu...

                bookId = foundBooks == null ? null : (int?)foundBooks.FirstOrDefault().BookId;
            }

            if(bookId != null)
            {
                var book = GetById((int)bookId);

                var bookAuthor = default(BookAuthor);

                var bookCategory = default(BookCategory);

                var author = _context.Authors.Where(a => a.Name.Equals(newAuthor)).FirstOrDefault();

                if (author != default(Author))
                {
                    bookAuthor = new BookAuthor
                    {
                        BookId = (int)bookId,
                        Book = book,
                        AuthorId = author.AuthorId,
                        Author = author
                    };
                }

                var category = _context.Categories.Where(c => c.Name.Equals(newCategory)).FirstOrDefault();

                if (category != default(Category))
                {
                    bookCategory = new BookCategory
                    {
                        BookId = (int)id,
                        Book = book,
                        Category = category,
                        CategoryId = category.CategoryId
                    };
                }

                var bookPublisher = _context.Publishers.Where(p => p.Name.Equals(newPublisher)).FirstOrDefault();

                if (book != default(Book))
                {
                    book.BookId = (int)id;
                    book.Title = newTitle.Equals("") ? book.Title : newTitle;
                    book.ISBN = newISBN == null ? book.ISBN : (int)newISBN;
                    book.Pages = newPages == null ? book.Pages : (int)newPages;
                    book.Publisher = newPublisher == "" ? book.Publisher : bookPublisher;
                    book.BookAuthors = newAuthor == "" ? book.BookAuthors : new List<BookAuthor> { bookAuthor };
                    book.BookCategory = newCategory == "" ? book.BookCategory : new List<BookCategory> { bookCategory };
                }

                _context.SaveChanges();
            }
        }
    }
}
