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
            IFormFile book, IFormFile bookCovering, int copiesCount=1)
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
                    Author = author,
                    CopiesCount = copiesCount
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

                for(int i=0; i<copiesCount; i++)//zoptymalizować zrobic dodawanie wszystkich za jednym zamachem
                {
                   AddCopy(bookToAdd, true, i==copiesCount-1);
                }

                var bookId = bookToAdd.BookId;

                var bookFileName = bookId.ToString() + "_book" + Path.GetExtension(book.GetFilename());

                var bookCoveringFileName = bookId.ToString() + "_covering" + Path.GetExtension(bookCovering.GetFilename());

                var savedBook = _context.Books.Where(b => b.BookId.Equals(bookId)).FirstOrDefault();

                savedBook.Path = bookFileName;
                savedBook.CoveringPath = bookCoveringFileName;

                _context.SaveChanges();

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

        public Copy AddCopy(Book book, bool isRented=true, bool saveChanges=false)
        {
            try
            {
                var copy = new Copy
                {
                    Book = book,
                    IsRented = isRented
                };
                _context.Copies.Add(copy);
                if(saveChanges)_context.SaveChanges();
                return copy;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
            try
            {
                return _context.Books.Where(b => b.BookId.Equals(id)).Select(b => b.Path).FirstOrDefault();
            }
            catch(ArgumentNullException e)
            {
                Console.WriteLine(e);
                return String.Empty;
            }
        }

        public Publisher GetPublisher(int id)
        {
            try
            {
                return _context.Publishers.Where(p => p.Name.Equals
                (p.Books.Where(b => b.BookId.Equals(id))
                .FirstOrDefault())).FirstOrDefault();
            }
            catch(ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public string GetTitle(int id)
        {
            try
            {
                return _context.Books.Where(b => b.BookId.Equals(id)).Select(b => b.Title).FirstOrDefault();
            }
            catch(ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return String.Empty;
            }
        }

        public Publisher AddPublisher(string name, string city)
        {
            try
            {
                if (name != "" && name != null)
                {
                    var publisher = _context.Publishers.Where(p => p.Name.Contains(name)).FirstOrDefault();

                    if (publisher == default(Publisher))
                    {
                        publisher = new Publisher { Name = name };
                        if (city != "" && city != null)
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
            catch(ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return default(Publisher);
            }

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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IEnumerable<Copy> GetBookCopies(Book book)
        {
            try
            {
                var tmp = _context.Copies.Where(c => c.BookId.Equals(book.BookId)).ToList();
                return tmp;
            }
            catch(ArgumentNullException e)
            {
                Console.WriteLine(e.Message); 
                return default(IEnumerable<Copy>);
            }
        }

        public bool AddLoan(Loan loan)
        {
            try
            {
                if (loan.Copy != null && loan.User != null)
                {
                    _context.Loans.Add(loan);
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public Loan AddLoan(ApplicationUser user, Copy copy, int loanDurationDays = 7)
        {
            try
            {
                var loan = new Loan
                {
                    Copy = copy,
                    CopyId = copy.CopyId,
                    StartDate = DateTime.Now,
                    LoanDurationDays = loanDurationDays,
                    User = user,
                    UserId = user.Id
                };
                _context.Loans.Add(loan);
                _context.SaveChanges();
                return loan;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<Copy> GetAvailableBookCopies(Book book)
        {
            try
            {
                var allBookCopies = GetBookCopies(book).ToList();
                if(allBookCopies.Where(c => c.IsRented.Equals(false)).Any())
                {
                    return allBookCopies;
                }
                else
                {
                    List<Loan> loans = new List<Loan>(); 
                    foreach(var copy in allBookCopies)
                    {
                        loans.Add(_context.Loans.Where(c => copy.CopyId.Equals(copy.CopyId)).Include(c => c.Copy).FirstOrDefault());
                    }
                    foreach(var loan in loans)
                    {
                        if(loan.StartDate.AddDays(loan.LoanDurationDays) >= DateTime.Now)
                        {
                            allBookCopies.Add(loan.Copy);
                        }
                        else
                        {
                            loans.Remove(loan);
                        }
                    }
                    if(allBookCopies.Any())
                    {
                        return allBookCopies;
                    }
                    return null;
                    //WAŻNE: PRZETESTOWAĆ
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<Copy> GetUserLoanCopies(ApplicationUser user)
        {
            try
            {
                var query = from loan in _context.Loans join 
                        copy in _context.Copies on loan.CopyId
                        equals copy.CopyId where loan.UserId.Equals(user.Id)
                        select copy;
                return query;
                //return _context.Loans.Where(l => l.UserId.Equals(user.Id)).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool DecrementBookCopy(Book book)
        {
            try
            {
                var bookToUpdate = _context.Books.Where(b => b.BookId.Equals(book.BookId)).FirstOrDefault();
                if (bookToUpdate != null)
                {
                    bookToUpdate.CopiesCount--;
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool IncrementBookCopy(Book book)
        {
            try
            {
                var bookToUpdate = _context.Books.Where(b => b.BookId.Equals(book.BookId)).FirstOrDefault();
                if (bookToUpdate != null)
                {
                    bookToUpdate.CopiesCount++;
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool RemoveUserLoan(ApplicationUser applicationUser, Book loanBook)
        {
            try
            {
                var loanToRemove = (from loan in _context.Loans join user
                                   in _context.ApplicationUsers on loan.UserId equals user.Id
                                   join copy in _context.Copies on loan.CopyId equals copy.CopyId
                                   join book in _context.Books on copy.BookId equals book.BookId
                                   where applicationUser.Id.Equals(user.Id) && loanBook.BookId.Equals(book.BookId)
                                   select loan).FirstOrDefault();

                if (loanToRemove != null)
                {
                    /*loanToRemove.Copy.IsRented = false;*/ //to nie wiedomo czy zadziała
                    var copy = _context.Copies.Where(c => c.CopyId.Equals(loanToRemove.CopyId)).FirstOrDefault();
                    if(copy != null)
                    {
                        copy.IsRented = false;
                        _context.SaveChanges();
                    }
                    _context.Remove(loanToRemove);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Loan GetLoanByCopy(Copy copy)
        {
            try
            {
                return _context.Loans.Where(l => l.CopyId.Equals(copy.CopyId)).FirstOrDefault();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
       }
    }
}
