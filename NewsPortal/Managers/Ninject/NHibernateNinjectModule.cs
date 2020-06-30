using Ninject.Modules;
using Ninject.Web.Common;
using System.Web.Mvc;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositories;
using Data_Access_Layer.Repositories.NHibernate;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace NewsPortal.Managers.Ninject
{
    public class NHibernateNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Rebind<UnitOfWork>().ToSelf().InRequestScope();
            Unbind<ModelValidatorProvider>();
            Rebind<IUserRepository>().To<UserRepository>().InRequestScope();
            Rebind<ICommentRepository>().To<CommentRepository>().InRequestScope();
            Rebind<INewsRepository>().To<NewsRepository>().InRequestScope();
            Rebind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<UnitOfWork>());
            Rebind<INhibernateUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<UnitOfWork>());
        }
    }
}