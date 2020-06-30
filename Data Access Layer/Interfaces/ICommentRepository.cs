using Data_Access_Layer.Entities;
using System.Collections.Generic;

namespace Data_Access_Layer.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IList<Comment> GetSetByNewsId(int newsId, int setIndex, int setCapacity);

        int GetCountByNewsId(int newsId);

        IList<Comment> GetByNewsId(int newsId);

    }
}
