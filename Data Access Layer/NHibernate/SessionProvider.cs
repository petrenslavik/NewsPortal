using System;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace Data_Access_Layer.NHibernate
{
    public class SessionProvider
    {
        private static volatile ISessionFactory _factory;
        private static readonly object SyncRoot = new object();

        public static ISession OpenSession()
        {
            if (_factory != null) return _factory.OpenSession();
            lock (SyncRoot)
            {
                if (_factory == null)
                {
                    CreateFactory();
                }
            }
            return _factory.OpenSession();
        }

        private static void CreateFactory()
        {
            var configuration = new Configuration();
            configuration.Configure();
            _factory = configuration.BuildSessionFactory();
        }
    }
}