using System;
using System.Collections.Generic;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.NHibernate
{
    public class NewsRepository : BaseRepository<NewsItem>, INewsRepository
    {
        public NewsRepository(INhibernateUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IList<NewsItem> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            if (setDate == DateTime.MinValue)
                return Session.QueryOver<NewsItem>().OrderBy(x => x.PublishDate).Desc.Skip(setIndex * setCapacity).Take(setCapacity).List();
            return Session.QueryOver<NewsItem>().Where(x => x.PublishDate.Date == setDate.Date).OrderBy(x => x.PublishDate).Desc.Skip(setIndex * setCapacity).Take(setCapacity).List();
        }

        public int GetUserId(int newsId)
        {
            return Session.Get<NewsItem>(newsId).UserId;
        }

        public int GetCountByDate(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return GetCount();
            return Session.QueryOver<NewsItem>().Where(x => x.PublishDate.Date == dateTime.Date).List().Count;
        }
    }
}