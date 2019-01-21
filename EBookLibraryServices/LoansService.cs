using System;
using EBookLibraryData;
using EBookLibraryData.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EBookLibraryServices
{
    public class LoansService : ILoans
    {
        private Context _context;
        public LoansService(Context context)
        {
            _context = context;
        }
        public bool Remove(int id = -1)
        {
            try
            {
                if (id > 0)
                {
                    var loan = _context.Loans.Where(a => a.LoanId.Equals(id));
                    if (loan.Any())
                    {
                        _context.Remove(loan.FirstOrDefault());
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
        public IEnumerable<Loan> GetMany(int take = 1000)
        {
            try
            {
                var loans = _context.Loans.Take(take);
                if (loans.Any())
                {
                    return loans.ToList();
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<Loan> GetLoansByBook(Book book)
        {
            try
            {
                var loans = _context.Loans.Where(l => l.Copy.BookId.Equals(book.BookId))
                    .Include(l => l.Copy).ThenInclude(l => l.Book).Include(b => b.User);
                if (loans.Any())
                {
                    return loans.ToList();
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
