using System;
using System.Collections.Generic;
using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BusinessEntity
    {
        IList<TEntity> GetAll();

        IList<TEntity> GetSet(int setIndex, int setCapacity, DateTime setDate); //partly

        TEntity Get(int id);

        int GetCount();

        void Create(TEntity entity);

        void Update(TEntity updatedEntity);

        void Delete(int id);
    }
}
