using EBookLibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibraryServices
{
    public class FileManageService : IFileManage
    {
        public async Task<bool> UploadFile(IFormFile file, string fileName, string filePath)
        {
            if (file == null || file.Length.Equals(0))
            {
                return false;
            }
            else
            {
                var directory = Path.Combine(
                Directory.GetCurrentDirectory(), filePath,//"library_assets", wwwroot\\images
                file.Name);

                var path = directory +"\\" + fileName;

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return true;
            }
        }
    }
}
