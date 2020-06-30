using System;
using System.Web;
using System.IO;
using System.Web.Configuration;

namespace NewsPortal.Managers
{
    public class FileManager
    {
        public static void SaveImage(HttpPostedFileBase picture, string filename)
        {
            if (!(picture.ContentType == "image/png" || picture.ContentType == "image/jpeg" || picture.ContentType == "image/bmp" || picture.ContentType == "image/gif"))
                throw new ArgumentException("Error, given file is not a .jpeg, .png, .gif or .bmp image.");
 
            string savePath = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["imagesPath"]  +filename);
            picture.SaveAs(savePath);
        }

        public static string GetUniqueFilename(string filename)
        {
            var guid = Guid.NewGuid();
            filename = Path.GetFileNameWithoutExtension(filename) + "-" + guid.ToString() + Path.GetExtension(filename);
            return filename;
        }
    }
}