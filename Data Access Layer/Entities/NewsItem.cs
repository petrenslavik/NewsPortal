using System;

namespace Data_Access_Layer.Entities
{
    [Serializable]
    public class NewsItem : BusinessEntity
    {
        public virtual int UserId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Text { get; set; }

        public virtual DateTime PublishDate { get; set; }

        public virtual string ImageSrc { get; set; }
    }
}