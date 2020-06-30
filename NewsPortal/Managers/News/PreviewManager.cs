using NewsPortal.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace NewsPortal.Managers
{
    public class PreviewManager
    {
        public static string PreviewPicture(NewsItemViewModel item, HttpPostedFileBase picture)
        {
            if (picture == null)
                return item.ImageSrc ?? WebConfigurationManager.AppSettings["defaultImagePath"];

            byte[] imageData = new byte[picture.ContentLength];
            using (MemoryStream target = new MemoryStream())
            {
                picture.InputStream.CopyTo(target);
                imageData = target.ToArray();
            }
            picture.InputStream.Read(imageData, 0, picture.ContentLength);
            string base64 = Convert.ToBase64String(imageData);
            string imgSrc = $"data:image;base64,{base64}";
            return imgSrc;
        }
    }
}