using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Route.C41.MVC.PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFileAsync(IFormFile file,string FolderName)
        {
            //string folderPath = $"C:\\Users\\Fatma\\Desktop\\ROUTE\\Backend\\tasks\\MVC\\new\\Route.C41.MVC\\Route.C41.MVC.PL\\wwwroot\\Files\\{FolderName}";
            //string folderPath=$"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string fileName=$"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath=Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return fileName;

        }
        public static void DeleteFile(string fileName, string FolderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName,fileName);
            if(File.Exists(folderPath))
                File.Delete(folderPath);

        }
    }
}
