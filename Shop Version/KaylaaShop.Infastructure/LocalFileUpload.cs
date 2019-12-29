
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;



namespace KaylaaShop.Infastructure
{
    public interface ILocalFileUpload
    {
        public  Task<string> WriteFile(IFormFile file,string FolderPath, string filename);
        public string WriteFileBase64(string ImgStr, string FolderPath, string filename);
    }
    public class LocalFileUpload :ILocalFileUpload
    {
      
        public async Task<string> WriteFile(IFormFile file, string FolderPath, string filename)
        {
            using (var stream = System.IO.File.Create(FolderPath))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
          
        }

        public string  WriteFileBase64(string ImgStr, string FolderPath, string filename)
        {
            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(FolderPath, imageBytes);

            return filename;
        }
    }
}
