using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Providers.Memory;

namespace Data_Access_Layer.Repositories.MemoryList
{
    public class BaseMemoryListRepository<TEntity> : IRepository<TEntity> where TEntity : BusinessEntity, new()
    {
        protected IUnitOfWork _unitOfWork;
        protected List<TEntity> storage;
        protected static int idIndexator = 1;

        public BaseMemoryListRepository()
        {
            storage = MemoryListProvider.GetList<TEntity>();
        }
        
        public IList<TEntity> GetAll()
        {
            return storage;
        }

        public virtual IList<TEntity> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            var entitySet = storage.Skip(setIndex * setCapacity).Take(setCapacity);

            return entitySet.ToList();
        }

        public TEntity Get(int id)
        {
            var entity = storage.FirstOrDefault(x => x.Id == id);

            return entity;
        }

        public int GetCount()
        {
            return storage.Count;
        }

        public void Create(TEntity entity)
        {
            entity.Id = idIndexator++;
            storage.Add(entity);
        }

        public void Update(TEntity updatedEntity)
        {
            var entity = storage.FirstOrDefault(x => x.Id == updatedEntity.Id);

            storage.Remove(entity);
            storage.Add(updatedEntity);
        }

        public virtual void Delete(int id)
        {
            var entity = storage.FirstOrDefault(x => x.Id == id);

            storage.Remove(entity);
        }
    }
}