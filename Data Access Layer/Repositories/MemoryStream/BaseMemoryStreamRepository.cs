using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Providers.Memory;

namespace Data_Access_Layer.Repositories.MemoryStream
{
    public class BaseMemoryStreamRepository<TEntity> : IRepository<TEntity> where TEntity :  BusinessEntity, new()
    {
        protected System.IO.MemoryStream stream;
        protected BinaryFormatter formatter;
        protected static int idIndexator = 1;

        public BaseMemoryStreamRepository()
        {
            formatter = new BinaryFormatter();
            stream = MemoryStreamProvider.GetStream<TEntity>();
        }

        public IList<TEntity> GetAll()
        {
            if (stream.Length == 0)
                return new List<TEntity>();

            stream.Position = 0;
            return (List<TEntity>)formatter.Deserialize(stream);
        }

        public virtual IList<TEntity> GetSet(int setIndex, int setCapacity, DateTime setDate)
        {
            if (stream.Length == 0)
                return new List<TEntity>();

            var entityList = GetAll();
            var entitySet = entityList.Skip(setIndex * setCapacity).Take(setCapacity);

            return entitySet.ToList();
        }

        public TEntity Get(int id)
        {
            if (stream.Length == 0)
                return null;

            var entityList = GetAll();
            var entity = entityList.Where(x => x.Id == id).FirstOrDefault();

            return entity;
        }

        public int GetCount()
        {
            if (stream.Length == 0)
                return 0;

            var entityList = GetAll();
            return entityList.Count;
        }

        public void Create(TEntity entity)
        {
            var entityList = (stream.Length != 0) ? GetAll() : new List<TEntity>();

            entity.Id = idIndexator++;
            entityList.Add(entity);
            stream.SetLength(0);
            formatter.Serialize(stream, entityList);
        }

        public void Update(TEntity updatedEntity)
        {
            if (stream.Length == 0)
                throw new IOException();

            var entityList = GetAll();
            var entity = entityList.Where(x => x.Id == updatedEntity.Id).FirstOrDefault();

            entityList.Remove(entity);
            entityList.Add(updatedEntity);

            stream.SetLength(0);
            formatter.Serialize(stream, entityList);
        }

        public virtual void Delete(int id)
        {
            if (stream.Length == 0)
                throw new IOException();

            var entityList = GetAll();
            var entity = entityList.Where(x => x.Id == id).FirstOrDefault();

            entityList.Remove(entity);

            stream.SetLength(stream.Length - 1);
            stream.Position = 0;
            formatter.Serialize(stream, entityList);
        }
    }
}