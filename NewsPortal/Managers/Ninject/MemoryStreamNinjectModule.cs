using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Web.Mvc;
using Data_Access_Layer.Repositories.MemoryStream;
using Data_Access_Layer.Repositories.NHibernate;
using Ninject;

namespace NewsPortal.Managers.Ninject
{
    public class MemoryStreamNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Unbind<ModelValidatorProvider>();
            Rebind<IUserRepository>().To<UserRepository>().InRequestScope();
            Rebind<ICommentRepository>().To<CommentsMemoryStreamRepository>().InRequestScope();
            Rebind<INewsRepository>().To<NewsMemoryStreamRepository>().InRequestScope();
            Rebind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<UnitOfWork>());
            Rebind<INhibernateUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<UnitOfWork>());
        }
    }
}