using EBookLibraryData;
using EBookLibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookLibraryServices
{
    public class QueueService: IQueue
    {
        private Context _context;
        public QueueService(Context context)
        {
            _context = context;
        }

        public bool AddQueue(ApplicationUser user, Book book)
        {
            try
            {
                if(user != null && book != null)
                {
                    _context.Queues.Add(new Queue
                    {
                        User = user,
                        Book = book,
                        JoinDate = DateTime.Now
                    });
                    _context.SaveChanges();
                }
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Queue GetQueue(ApplicationUser user, Book book)
        {
            try
            {
                if(user != null && book != null)
                {
                    var queue = _context.Queues.Where(q => q.UserId.Equals(user.Id) && q.BookId.Equals(book.BookId));
                    if (queue.Any()) return queue.FirstOrDefault();
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool RemoveQueue(Queue queue)
        {
            try
            {
                if(queue != null)
                {
                    _context.Queues.Remove(queue);
                    _context.SaveChanges();
                }
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool RemoveById(int id = -1)
        {
            try
            {
                if (id > 0)
                {
                    var queue = _context.Queues.Where(a => a.QueueId.Equals(id));
                    if (queue.Any())
                    {
                        _context.Remove(queue.FirstOrDefault());
                        _context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IEnumerable<Queue> GetQueuesForBook(Book book)
        {
            try
            {
                if (book != null)
                {
                    var queues = _context.Queues.Where(b => b.BookId.Equals(book.BookId)).OrderBy(b => b.JoinDate);
                    return queues;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool CheckIfAlreadyInQueue(ApplicationUser user, Book book)
        {
            try
            {
                return _context.Queues.Where(q => q.UserId.Equals(user.Id) && q.BookId.Equals(book.BookId)).Any();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IEnumerable<Book> GetAllUserBooksInQueue(ApplicationUser user)
        {
            try
            {
                if(user != null)
                {
                    var books = _context.Queues.Where(q => q.UserId.Equals(user.Id)).Include(q => q.Book).Select(q => q.Book);
                    if (books.Any()) return books.ToList();
                }
                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<Queue> GetMany(int take = 1000)
        {
            try
            {
                var queues = _context.Queues.Take(take);
                if (queues.Any())
                {
                    return queues.ToList();
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
