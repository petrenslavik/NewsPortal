using Data_Access_Layer.Identity;
using NHibernate;

namespace Data_Access_Layer.Interfaces
{
    public  interface IUnitOfWork
    {
        void BeginTransaction();

        void Commit();
    }
}