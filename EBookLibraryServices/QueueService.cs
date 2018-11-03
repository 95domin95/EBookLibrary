using EBookLibraryData;
using EBookLibraryData.Models;
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
    }
}
