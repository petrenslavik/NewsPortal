using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.MemoryStream
{
    public class CommentsMemoryStreamRepository : BaseMemoryStreamRepository<Comment>, ICommentRepository
    {
        public CommentsMemoryStreamRepository() : base()
        {
        }

        public IList<Comment> GetByNewsId(int newsId)
        {
            if (stream.Length == 0)
                return new List<Comment>();

            var entityList = GetAll();
            return entityList.Where(item => item.NewsId == newsId).OrderBy(c => c.PublishDate).ToList();
        }

        public int GetCountByNewsId(int newsId)
        {
            if (stream.Length == 0)
                return 0;

            var entityList = GetAll();
            return entityList.Where(item => item.NewsId == newsId).Count();
        }

        public IList<Comment> GetSetByNewsId(int newsId, int setIndex, int setCapacity)
        {
            if (stream.Length == 0)
                return new List<Comment>();

            var entityList = GetAll();
            return entityList.Where(item => item.NewsId == newsId).OrderByDescending(item => item.PublishDate).Skip(setIndex * setCapacity).Take(setCapacity).ToList();
        }
    }
}