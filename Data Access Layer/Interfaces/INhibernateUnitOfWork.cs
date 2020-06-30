using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Data_Access_Layer.Interfaces
{
    public interface INhibernateUnitOfWork:IUnitOfWork
    {
        ISession Session { get; }
    }
}
