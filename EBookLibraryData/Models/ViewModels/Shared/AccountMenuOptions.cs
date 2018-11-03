using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.AccountManage
{
    public class AccountMenuOptions
    {
        public struct MenuOption
        {
            public string Name { get; set; }
            public string CommonName { get; set; }
        }
        public static MenuOption[] Options
        {
            get
            {
                return new MenuOption[] {
                    new MenuOption{
                        Name = "LoanHistory",
                        CommonName = "Historia Wyporzyczeń",
                    },
                    new MenuOption{
                        Name = "InQueue",
                        CommonName = "Oczekujące"
                    },
                    new MenuOption{
                        Name = "Loaned",
                        CommonName = "Wyporzyczone"
                    }
                };
            }
        }
    }
}
