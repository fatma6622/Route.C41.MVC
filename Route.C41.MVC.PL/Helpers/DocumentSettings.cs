using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Route.C41.MVC.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string FolderName)
        {
            //string folderPath = $"C:\\Users\\Fatma\\Desktop\\ROUTE\\Backend\\tasks\\MVC\\new\\Route.C41.MVC\\Route.C41.MVC.PL\\wwwroot\\Files\\{FolderName}";
            //string folderPath=$"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string fileName=$"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath=Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
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
