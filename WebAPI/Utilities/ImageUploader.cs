using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utilities
{
    public class ImageUploader
    {
        public static Task<string> ImageUpload(string path, IFormFile file)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    var randomFileName = Path.GetRandomFileName();
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();
                    var fileName = Path.ChangeExtension(randomFileName, fileExtension);
                    var filePath = Path.Combine(path, fileName);

                    using (FileStream fileStream = File.Create(filePath))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        return fileName;
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        public static Task ImageDelete(string path)
        {
            return Task.Run(() =>
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    throw new Exception("Seçili dosya silinemedi! " + ex.Message);
                }
            });
        }
    }
}
