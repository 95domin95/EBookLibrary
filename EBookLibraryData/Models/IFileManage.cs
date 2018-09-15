using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibraryData.Models
{
    public interface IFileManage
    {
        Task<bool> UploadFile(IFormFile file, string fileName);
    }
}
