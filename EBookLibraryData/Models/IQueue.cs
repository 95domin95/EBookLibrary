using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface IQueue
    {
        bool AddQueue(ApplicationUser user, Book book);
        Queue GetQueue(ApplicationUser user, Book book);
        bool RemoveQueue(Queue queue);
        IEnumerable<Queue> GetQueuesForBook(Book book);
        bool CheckIfAlreadyInQueue(ApplicationUser user, Book book);
    }
}
