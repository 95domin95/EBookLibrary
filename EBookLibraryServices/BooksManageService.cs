using EBookLibraryData;
using EBookLibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibraryServices
{
    public class BooksManageService : IBooksManage
    {
        private Context _context;
        private IFileManage _file;
        public BooksManageService(Context context, IFileManage file)
        {
            _context = context;
            _file = file;
        }
        public async Task<bool> Add(string title, int? ISBN, int? pages,
            string author, string publisher, string category,
            IFormFile book, IFormFile bookCovering)
        {
            if (title != "" && category != "" && book != default(IFormFile))
            {
                var publisherResult = _context.Publishers.Where(p => p.Name.Contains(publisher)).FirstOrDefault();

                var categoryResult = _context.Categories.Where(c => c.Name.Contains(category)).FirstOrDefault();

                var bookToAdd = new Book
                {
                    Title = title,
                    ISBN = ISBN,
                    Pages = pages,
                    Author = author
                };

                if(publisherResult != default(Publisher))
                {
                    bookToAdd.Publisher = publisherResult;
                }
                else
                {
                    bookToAdd.Publisher = AddPublisher(publisher, "");
                }

                if(categoryResult != default(Category))
                {
                    bookToAdd.Category = categoryResult;
                }

                _context.Books.Add(bookToAdd);
                _context.SaveChanges();

                var bookId = bookToAdd.BookId;

                var bookFileName = bookId.ToString() + "_book" + Path.GetExtension(book.GetFilename());

                var bookCoveringFileName = bookId.ToString() + "_covering" + Path.GetExtension(bookCovering.GetFilename());

                var savedBook = _context.Books.Where(b => b.BookId.Equals(bookId)).FirstOrDefault();

                savedBook.Path = bookFileName;
                savedBook.CoveringPath = bookCoveringFileName;

                await _context.SaveChangesAsync();

                if (!(await _file.UploadFile(book, bookFileName) && await _file.UploadFile(bookCovering, bookCoveringFileName)))
                {
                    var bookToDelete = _context.Books.Where(b => b.BookId.Equals(bookId)).FirstOrDefault();

                    if(bookToDelete != default(Book))
                    {
                        _context.Books.Remove(bookToDelete);
                        _context.SaveChanges();
                    }

                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                var count = _context.Books.Where(b => b.BookId.Equals(id)).Count();

                if (count > 0)
                {
                    _context.Books.Remove(GetById(id));
                    _context.SaveChanges();
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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
            List<Book> result = new List<Book>();
            if(_context.Books.Count() > 0)
            {
                result = _context.Books.ToList();
            }
            if (ISBN != null) result = result.Where(b => b.ISBN.Equals(ISBN)).ToList();
            if (title != "" && title != null) result = result.Where(b => b.Title.Contains(title)).ToList();
            if (author != "" && author != null) result = result.Where(b => b.Author.Contains(author)).ToList();
            if (pagesMax != null) result = result.Where(b => b.Pages < pagesMax).ToList();
            if (pagesMin != null) result = result.Where(b => b.Pages > pagesMin).ToList();
            if (publisher != "" && publisher != null)
            {
                var publisherId = _context.Publishers.Where(p => p.Name.Equals(publisher))
                    .Select(p => p.PublisherId).FirstOrDefault();

                result = result.Where(b => b.PublisherId.Equals(publisherId)).ToList();
            }
            if (category != "" && category != null)
            {
                var categoryId = _context.Categories.Where(c => c.Name.Equals(category))
                    .Select(c => c.CategoryId).FirstOrDefault();

                result = result.Where(b => b.CategoryId.Equals(categoryId)).ToList();
            }

            return result.Count() > 0 ? result : default(List<Book>);
        }

        public Book GetById(int id)
        {
            return _context.Books.Where(b => b.BookId.Equals(id))
                .Include(b => b.Category).Include(b => b.Publisher).FirstOrDefault();
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

        public Publisher AddPublisher(string name, string city)
        {
            if(name != "" && name != null)
            {
                var publisher = _context.Publishers.Where(p => p.Name.Contains(name)).FirstOrDefault();

                if (publisher == default(Publisher))
                {
                    publisher = new Publisher {Name = name };
                    if(city != "" && city != null)
                    {
                        publisher.City = city;
                    }
                    _context.Publishers.Add(publisher);
                    _context.SaveChanges();
                    return publisher;
                }
            }
            return default(Publisher);
        }

        public bool UpdateById(int? id, string newTitle="", int? newISBN=null,
            string newAuthor="", int? newPages = null,
            string newPublisher="", string newCategory="")
        {
            try
            {
                if (id != null)
                {
                    var book = _context.Books.Where(b => b.BookId.Equals(id)).FirstOrDefault();

                    if (book != default(Book))
                    {
                        if (newTitle != "") book.Title = newTitle;
                        if (newISBN != null) book.ISBN = newISBN;
                        if (newAuthor != "") book.Author = newAuthor;
                        if (newPages != null) book.Pages = newPages;
                        if (newPublisher != "")
                        {
                            var publisher = _context.Publishers.Where(p => p.Name.Contains(newPublisher)).FirstOrDefault();

                            if (publisher != default(Publisher))
                            {
                                book.Publisher = publisher;
                            }
                        }
                        if (newCategory != "")
                        {
                            var category = _context.Categories.Where(c => c.Name.Contains(newCategory)).FirstOrDefault();

                            if (category != default(Category))
                            {
                                book.Category = category;
                            }
                        }
                        _context.Update(book);
                        _context.SaveChanges();
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
