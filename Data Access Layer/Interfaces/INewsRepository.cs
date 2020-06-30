using Data_Access_Layer.Entities;
using System;

namespace Data_Access_Layer.Interfaces
{
    public interface INewsRepository : IRepository<NewsItem>
    {
        int GetUserId(int newsId);

        int GetCountByDate(DateTime dateTime);
    }
}
