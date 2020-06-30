using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.MemoryStream
{
    public class UsersMemoryStreamRepository : BaseMemoryStreamRepository<User>, IUserRepository
    {
        public UsersMemoryStreamRepository() : base()
        {
        }

        public User Get(string userName)
        {
            if (stream.Length == 0)
                return null;

            var entityList = GetAll();
            return entityList.Where(x => x.UserName == userName).SingleOrDefault();
        }

        public User GetByEmail(string email)
        {
            if (stream.Length == 0)
                return null;

            var entityList = GetAll();
            return entityList.Where(x => x.Email == email).SingleOrDefault();
        }

        public override IList<User> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            if (stream.Length == 0)
                return new List<User>();

            var entityList = GetAll();
            var entitySet = entityList.OrderBy(x => x.UserName).Skip(setIndex * setCapacity).Take(setCapacity);
            return entitySet.ToList();
        }
    }
}