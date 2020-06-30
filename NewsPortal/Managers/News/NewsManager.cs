using System.Web;
using System.Web.Configuration;
using System.IO;
using Data_Access_Layer.Entities;

namespace NewsPortal.Managers
{
    public class NewsManager
    {
        public static void SaveNewsPicture(NewsItem newsItem, HttpPostedFileBase picture)
        { 
            if (picture == null)
            {
                newsItem.ImageSrc = WebConfigurationManager.AppSettings["defaultImagePath"];
            }
            else
            {
                string pictureFilename = FileManager.GetUniqueFilename(picture.FileName);
                FileManager.SaveImage(picture, pictureFilename);
                newsItem.ImageSrc = WebConfigurationManager.AppSettings["imagesPath"] + pictureFilename;
            }
        }

        public static void UpdateNewsPicture (NewsItem updatedItem, HttpPostedFileBase picture, NewsItem oldItem)
        {
            if (picture != null)
            {
                string pictureFilename = FileManager.GetUniqueFilename(picture.FileName);
                FileManager.SaveImage(picture, pictureFilename);
                updatedItem.ImageSrc = WebConfigurationManager.AppSettings["imagesPath"] + pictureFilename;
            }

            if (File.Exists(HttpContext.Current.Server.MapPath(oldItem.ImageSrc)) && (oldItem.ImageSrc != updatedItem.ImageSrc) && (oldItem.ImageSrc.Trim() != WebConfigurationManager.AppSettings["defaultImagePath"].Trim()))
                File.Delete(HttpContext.Current.Server.MapPath(oldItem.ImageSrc));
        }

        public static void DeleteNewsPicture(NewsItem newsItem)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath(newsItem.ImageSrc)) && (newsItem.ImageSrc.Trim() != WebConfigurationManager.AppSettings["defaultImagePath"].Trim()))
                File.Delete(HttpContext.Current.Server.MapPath(newsItem.ImageSrc));
        }
    }
}