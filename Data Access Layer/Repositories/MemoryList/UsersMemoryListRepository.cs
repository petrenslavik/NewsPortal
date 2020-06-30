using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;

namespace Data_Access_Layer.Repositories.MemoryList
{
    public class UsersMemoryListRepository : BaseMemoryListRepository<User>, IUserRepository
    {
        public User Get(string userName)
        {
            return storage.Where(x => x.UserName == userName).SingleOrDefault();
        }

        public User GetByEmail(string email)
        {
            return storage.Where(x => x.Email == email).SingleOrDefault();
        }

        public override IList<User> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            var entitySet = storage.OrderBy(x => x.UserName).Skip(setIndex * setCapacity).Take(setCapacity);

            return entitySet.ToList();
        }
    }
}