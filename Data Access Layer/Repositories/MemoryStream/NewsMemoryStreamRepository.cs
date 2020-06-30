using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.MemoryStream
{
    public class NewsMemoryStreamRepository : BaseMemoryStreamRepository<NewsItem>, INewsRepository
    {
        public NewsMemoryStreamRepository() : base()
        {
        }

        public override IList<NewsItem> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            if (stream.Length == 0)
                return new List<NewsItem>();

            var entityList = GetAll();
            var entitySet = entityList.OrderByDescending(x => x.PublishDate).Skip(setIndex * setCapacity).Take(setCapacity);
            return entitySet.ToList();
        }

        public int GetUserId(int newsId)
        {
            if (stream.Length == 0)
                throw new IOException();

            var entityList = GetAll();
            var userId = entityList.Where(x => x.Id == newsId).Select(x => x.UserId).FirstOrDefault();
            return userId;
        }

        public int GetCountByDate(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return GetCount();
            return GetAll().Where(x => x.PublishDate.Date == dateTime.Date).ToList().Count;
        }
    }
}