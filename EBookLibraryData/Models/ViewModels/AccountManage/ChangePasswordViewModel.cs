using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.AccountManage
{
    public class ChangePasswordViewModel
    {
        public bool ChangedSuccessfully { get; set; } = false;
        public bool ChangedError { get; set; } = false;
        public string ChangedSuccess
        {
            get
            {
                return "Pomyślnie zmieniono hasło użytkownika";
            }
        }
        public string ChangedFailed
        {
            get
            {
                return "Nie udało się zmienić hasła użytkownika";
            }
        }
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Aktualne hasło")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć conajmniej {2} i maksymalnie {1} znaków długości.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła się nie zgadzają.")]
        public string ConfirmPassword { get; set; }
    }

}
