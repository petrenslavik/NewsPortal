using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string userName);

        User GetByEmail(string email);
    }
}
