using System;
using EBookLibraryData;
using EBookLibraryData.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookLibraryServices
{
    public class PublishersService: IPublishers
    {
        private Context _context;
        public PublishersService(Context context)
        {
            _context = context;
        }
        public bool Remove(int id = -1)
        {
            try
            {
                if (id > 0)
                {
var publisher = _context.Publishers
                        .Where(a => a.PublisherId.Equals(id));
if (publisher.Any())
{
    _context.Remove(publisher.FirstOrDefault());
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

        public bool Add(string name)
        {

            try
            {
                if (name != null)
                {
                    var publisher = new Publisher
                    {
                        Name = name
                    };
                    _context.Publishers.Add(publisher);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IEnumerable<Publisher> GetMany(int take=1000)
        {
            try
            {
                var publishers = _context.Publishers.Take(take);
                if(publishers.Any())
                {
                    return publishers.ToList();
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
