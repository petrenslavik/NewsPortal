using System;

namespace Data_Access_Layer.Entities
{
    [Serializable]
    public class Comment: BusinessEntity
    {
        public virtual int UserId { get; set; }

        public virtual int NewsId { get; set; }

        public virtual string Text { get; set; }

        public virtual DateTime PublishDate { get; set; }
    }
}