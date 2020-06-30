using System;
using System.Collections.Generic;
using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Providers.Memory
{
    public class MemoryListProvider
    {
        private static List<NewsItem> _news;
        private static List<User> _users;
        private static List<Comment> _comments;

        private static object syncRoot = new object();

        private static List<NewsItem> News {
            get {
                if (_news == null)
                {
                    lock (syncRoot)
                    {
                        if (_news == null)
                        {
                            _news = new List<NewsItem>();
                        }
                    }
                }
                return _news;
            }
        }

        private static List<User> Users {
            get {
                if (_users == null)
                {
                    lock (syncRoot)
                    {
                        if (_users == null)
                        {
                            _users = new List<User>();
                        }
                    }
                }
                return _users;
            }
        }

        private static List<Comment> Comments {
            get {
                if (_comments == null)
                {
                    lock (syncRoot)
                    {
                        if (_comments == null)
                        {
                            _comments = new List<Comment>();
                        }
                    }
                }
                return _comments;
            }
        }

        public static dynamic GetList<T>() where T : BusinessEntity, new()
        {
            T entity = new T();

            if (entity is NewsItem)
                return News;

            if (entity is User)
                return Users;

            if (entity is Comment)
                return Comments;

            throw new ArgumentException("There is no appropriate list for given type");
        }
    }
}