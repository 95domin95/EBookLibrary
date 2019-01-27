using System.ComponentModel.DataAnnotations;

namespace EBookLibraryData.Models.ViewModels.AccountManage
{
    public class ManageAccountViewModel
    {
        public bool ChangedSuccessfully { get; set; } = false;
        public bool ChangedError { get; set; } = false;
        public string ChangedSuccess
        {
            get
            {
                return "Pomyślnie zmieniono dane użytkownika";
            }
        }
        public string ChangedFailed
        {
            get
            {
                return "Nie udało się zmienić danych użytkownika";
            }
        }
        public ApplicationUser User { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Niepoprawny adres email")]
        [Display(Name = "Adress e-mail")]
        public string Email { get; set; }
    }
}
