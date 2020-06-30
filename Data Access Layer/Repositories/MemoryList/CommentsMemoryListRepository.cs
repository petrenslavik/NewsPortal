using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.MemoryList
{
    public class CommentsMemoryListRepository : BaseMemoryListRepository<Comment>, ICommentRepository
    {
        public IList<Comment> GetByNewsId(int newsId)
        {
            return storage.Where(item => item.NewsId == newsId).OrderBy(c => c.PublishDate).ToList();
        }

        public int GetCountByNewsId(int newsId)
        {
            return storage.Count(item => item.NewsId == newsId);
        }

        public IList<Comment> GetSetByNewsId(int newsId, int setIndex, int setCapacity)
        {
            return storage.Where(item => item.NewsId == newsId).OrderByDescending(item => item.PublishDate).Skip(setIndex * setCapacity).Take(setCapacity).ToList();
        }
    }
}