using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using NHibernate;

namespace Data_Access_Layer.Repositories.NHibernate
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BusinessEntity
    {

        public readonly INhibernateUnitOfWork _uow;

        protected ISession Session => _uow.Session;

        public BaseRepository(INhibernateUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public IList<TEntity> GetAll()
        {
            return Session.QueryOver<TEntity>().List();
        }

        public virtual IList<TEntity> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            return Session.QueryOver<TEntity>().Skip(setIndex * setCapacity).Take(setCapacity).List();
        }

        public TEntity Get(int id)
        {
            return Session.Get<TEntity>(id);
        }

        public int GetCount()
        {
            return Session.Query<TEntity>().Count();
        }

        public void Create(TEntity entity)
        {
            _uow.BeginTransaction();
            Session.Save(entity);
        }

        public void Update(TEntity updatedEntity)
        {
            _uow.BeginTransaction();
            Session.Update(updatedEntity);
        }

        public virtual void Delete(int id)
        {
            _uow.BeginTransaction();
            var entity = Session.Get<TEntity>(id);
            if (entity == null)
            return;
            Session.Delete(entity);
        }
    }
}