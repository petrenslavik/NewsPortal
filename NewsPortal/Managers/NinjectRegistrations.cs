using Ninject.Modules;
using System.Web.Mvc;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositories;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace NewsPortal.Managers
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Unbind<ModelValidatorProvider>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserRepository>().To<UserRepository>().Intercept().With<Interceptor>(); ;//.WithConstructorArgument("unitOfWork", new UnitOfWork());
            Bind<ICommentRepository>().To<CommentRepository>().Intercept().With<Interceptor>(); ;//.WithConstructorArgument("unitOfWork", new UnitOfWork());
            Bind<INewsRepository>().To<NewsRepository>().Intercept().With<Interceptor>(); ;//.WithConstructorArgument("unitOfWork", new UnitOfWork());
        }
    }
}