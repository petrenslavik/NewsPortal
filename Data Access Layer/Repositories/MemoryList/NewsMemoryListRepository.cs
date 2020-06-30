using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.MemoryList
{
    public class NewsMemoryListRepository : BaseMemoryListRepository<NewsItem>, INewsRepository
    {
        public override IList<NewsItem> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            if (setDate == DateTime.MinValue)
                return storage.OrderBy(x => x.PublishDate).Skip(setIndex * setCapacity).Take(setCapacity).ToList();
            return storage.Where(x => x.PublishDate.Date == setDate.Date).OrderBy(x => x.PublishDate).Skip(setIndex * setCapacity).Take(setCapacity).ToList();
        }

        public int GetUserId(int newsId)
        {
            var userId = storage.Where(x => x.Id == newsId).Select(x => x.UserId).FirstOrDefault();

            return userId;
        }

        public int GetCountByDate(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return GetCount();
            return storage.Where(x => x.PublishDate.Date == dateTime.Date).ToList().Count;
        }
    }
}