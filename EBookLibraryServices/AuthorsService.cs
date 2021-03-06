﻿using EBookLibraryData;
using EBookLibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookLibraryServices
{
    public class AuthorsService: IAuthors
    {
        private Context _context;
        public AuthorsService(Context context)
        {
            _context = context;
        }
        public bool Remove(int id=-1)
        {
            try
            {
                if(id > 0)
                {
                    var author = _context.Authors.Where(a => a.AuthorId.Equals(id));
                    if(author.Any())
                    {
                        _context.Remove(author.FirstOrDefault());
                        _context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch(Exception e)
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
                    _context.Authors.Add(new Author
                    {
                        Name = name
                    });
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
        public IEnumerable<Author> GetMany(int take = 1000)
        {
            try
            {
                var authors = _context.Authors.Take(take);
                if (authors.Any())
                {
                    return authors.ToList();
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
