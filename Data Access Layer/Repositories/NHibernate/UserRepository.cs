using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.NHibernate
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(INhibernateUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User Get(string userName)
        {
            return Session.QueryOver<User>().Where(u => u.UserName == userName).SingleOrDefault();
        }

        public User GetByEmail(string email)
        {
            return Session.QueryOver<User>().Where(u => u.Email == email).SingleOrDefault();
        }

        public override IList<User> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            return Session.Query<User>().OrderBy(x => x.UserName).Skip(setIndex * setCapacity).Take(setCapacity).ToList();

        }
    }
}