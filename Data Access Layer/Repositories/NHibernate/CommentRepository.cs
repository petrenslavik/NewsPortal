using System.Collections.Generic;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.NHibernate
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(INhibernateUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IList<Comment> GetByNewsId(int newsId)
        {
            return Session.QueryOver<Comment>().Where(c => c.NewsId == newsId).OrderBy(c => c.PublishDate).Asc.List();
        }

        public int GetCountByNewsId(int newsId)
        {
            return Session.QueryOver<Comment>().Where(item => item.NewsId == newsId).RowCount();
        }

        public IList<Comment> GetSetByNewsId(int newsId, int setIndex, int setCapacity)
        {
            return Session.QueryOver<Comment>().Where(item => item.NewsId == newsId).OrderBy(item => item.PublishDate).Desc.Skip(setIndex * setCapacity).Take(setCapacity).List();
        }
    }
}