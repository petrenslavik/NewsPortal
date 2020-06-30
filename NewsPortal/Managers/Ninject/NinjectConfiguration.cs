using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Managers.Ninject
{
    public enum Storage
    {
        Database,
        Memory
    }

    public class NinjectConfiguration
    {
        private static StandardKernel kernel;

        public static void Configure(Storage storage)
        {
            NinjectModule module;

            switch (storage)
            {
                case Storage.Database:
                    module = new NHibernateNinjectModule();
                    break;
                case Storage.Memory:
                    module = new MemoryListNinjectModule();
                    break;
                default:
                    throw new ArgumentNullException("Storage argument was null.");
            }

            kernel = new StandardKernel(module);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}