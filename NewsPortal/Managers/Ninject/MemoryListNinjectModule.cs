using Ninject.Modules;
using System.Web.Mvc;
using Ninject.Web.Common;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositories;
using Data_Access_Layer.Repositories.MemoryList;
using Data_Access_Layer.Repositories.NHibernate;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace NewsPortal.Managers.Ninject
{
    public class MemoryListNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Unbind<ModelValidatorProvider>();
            Rebind<IUserRepository>().To<UserRepository>();
            Rebind<ICommentRepository>().To<CommentsMemoryListRepository>();
            Rebind<INewsRepository>().To<NewsMemoryListRepository>();
            Rebind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<UnitOfWork>());
            Rebind<INhibernateUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<UnitOfWork>());
        }
    }
}