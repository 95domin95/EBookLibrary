using EBookLibraryData;
using EBookLibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookLibraryServices
{
    public class LoanHistoryService: ILoanHistory
    {
        private Context _context;
        public LoanHistoryService(Context context)
        {
            _context = context;
        }
        public bool AddLoanHistory(Loan loan, ApplicationUser user, Book book)
        {
            try
            {
                if(loan != null && user != null && book != null)
                {
                    _context.LoanHistories.Add(new LoanHistory
                    {
                        Book = book,
                        User = user,
                        LoanDate = loan.StartDate,
                    });
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

        public bool ModifyReturnDate(DateTime returnDate, LoanHistory loanHistory)
        {
            try
            {
                if(returnDate != null)
                {
                    loanHistory.ReturnDate = returnDate;
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

        public LoanHistory GetLoanHistory(ApplicationUser user, Book book)
        {
            try
            {
                if (user != null && book != null)
                {
                    var loanHistory = _context.LoanHistories
                        .Where(lh => lh.UserId.Equals(user.Id) && lh.BookId.Equals(book.BookId)).OrderByDescending(l => l.LoanDate);
                    if(loanHistory != null)
                    {
                        return loanHistory.FirstOrDefault();
                    }
                }
                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<LoanHistory> GetAllUserLoanHistories(ApplicationUser user)
        {
            try
            {
                if(user != null)
                {
                    var loanHistories = _context.LoanHistories.Where(lh => lh.UserId.Equals(user.Id)).Include(lh => lh.Book);
                    if(loanHistories.Any())
                    {
                        return loanHistories.ToList();
                    }
                }
                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<LoanHistory> GetMany(int take = 1000)
        {
            try
            {
                var loanHistories = _context.LoanHistories.Take(take);
                if (loanHistories.Any())
                {
                    return loanHistories.ToList();
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Remove(int id = -1)
        {
            try
            {
                if (id > 0)
                {
                    var loanHistory = _context.LoanHistories.Where(a => a.LoanHistoryId.Equals(id));
                    if (loanHistory.Any())
                    {
                        _context.Remove(loanHistory.FirstOrDefault());
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
    }
}
