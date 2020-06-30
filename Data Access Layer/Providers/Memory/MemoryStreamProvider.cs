using System;
using System.IO;
using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Providers.Memory
{
    public static class MemoryStreamProvider
    {
        private static MemoryStream _news;
        private static MemoryStream _users;
        private static MemoryStream _comments;

        private static object syncRoot = new object();

        private static MemoryStream News {
            get {
                if (_news == null)
                {
                    lock (syncRoot)
                    {
                        if (_news == null)
                        {
                            _news = new MemoryStream();
                        }
                    }
                }
                return _news;
            }
        }

        private static MemoryStream Users {
            get {
                if (_users == null)
                {
                    lock (syncRoot)
                    {
                        if (_users == null)
                        {
                            _users = new MemoryStream();
                        }
                    }
                }
                return _users;
            }
        }

        private static MemoryStream Comments {
            get {
                if (_comments == null)
                {
                    lock (syncRoot)
                    {
                        if (_comments == null)
                        {
                            _comments = new MemoryStream();
                        }
                    }
                }
                return _comments;
            }
        }

        public static MemoryStream GetStream<T>() where T : BusinessEntity, new()
        {
            T entity = new T();

            if (entity is NewsItem)
                return News;
            else
            if (entity is User)
                return Users;
            else
            if (entity is Comment)
                return Comments;

            throw new ArgumentException("There is no appropriate stream for given type");
        }

        public static void DisposeAllStreams()
        {
            _news?.Dispose();
            _users?.Dispose();
            _comments?.Dispose();
        }
    }
}