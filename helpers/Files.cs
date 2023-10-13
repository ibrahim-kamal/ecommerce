using e_commerce.models;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace e_commerce.helpers
{
    public class Files
    {

        private IHostingEnvironment Environment;
        public Files(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public String SaveImage(IFormFile file, String SavePath="uploads/products") {
            String filePath = SavePath+"/";

            string path = Path.Combine(this.Environment.WebRootPath, SavePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Guid.NewGuid().ToString("N")+Path.GetFileName(file.FileName);
            filePath += fileName;
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filePath;
        }


        public String SaveImageFromLink(String link, String SavePath = "uploads/products") {
            
            String filePath = SavePath+"/";
            string fileName = Guid.NewGuid().ToString("N") + Path.GetFileName(link);
            fileName = fileName.Split("?")[0];
            filePath += fileName;
            string path = Path.Combine(this.Environment.WebRootPath, filePath);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(link, path);
            }

            return filePath; 
        }
    }
}
