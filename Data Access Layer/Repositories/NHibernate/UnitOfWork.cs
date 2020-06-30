using System;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.NHibernate;
using NHibernate;

namespace Data_Access_Layer.Repositories.NHibernate
{
    public class UnitOfWork : INhibernateUnitOfWork
    {

        private ISession _session;
        private ITransaction _transaction;


        public ISession Session
        {
            get
            {
                if (_session == null || !_session.IsOpen)
                    _session = SessionProvider.OpenSession();
                return _session;
            }
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                return;
            Session.Dispose();
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Commit();
                }
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Rollback();
                }
            }
            finally
            {
                Session.Dispose();
            }
        }
    }
}