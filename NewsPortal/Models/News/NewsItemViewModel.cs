using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Data_Access_Layer.Entities;
using NewsPortal.Attributes;
using EngContent;

namespace NewsPortal.Models
{
    public class NewsItemViewModel
    {
        public NewsItemViewModel()
        {
        }


        public NewsItemViewModel(NewsItem newsItem)
        {
            Title = newsItem.Title;
            Id = newsItem.Id;
            Text = newsItem.Text;
            ImageSrc = newsItem.ImageSrc;
            PublishDate = newsItem.PublishDate;
            UserId = newsItem.UserId;
        }

        public int Id { get; set; }

        [StringLength(maximumLength: 150)]
        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_NewsItemView_Title_Required")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_NewsItemView_Text_Required")]
        public string Text { get; set; }

        public string ImageSrc { get; set; }

        [ImageSizeValidation(1024 * 1024, ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_NewsItemView_Picture_Size")]
        [ImageTypeValidation("jpeg","png", ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_NewsItemView_Picture_Type")]
        public HttpPostedFileBase Picture { get; set; }

        public DateTime PublishDate { get; set; }

        public int UserId { get; set; }

        public string UrlToken { get; set; }
    }
}