using Microsoft.AspNet.Identity;
using System;

namespace Data_Access_Layer.Entities
{
    [Serializable]
    public class User : BusinessEntity, IUser<int>
    {
        public virtual string UserName { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string Email { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual bool ConfirmEmail { get; set; }
    }
}