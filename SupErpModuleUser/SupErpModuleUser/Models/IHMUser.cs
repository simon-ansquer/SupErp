using SupErpModuleUser.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErpModuleUser.Helpers;

namespace SupErpModuleUser.Models
{
    public class IHMUser : IHMModel
    {

        public IHMUser()
        { }

        public IHMUser(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Address = user.Address;
            Role = user.Role.ToIHMRole();
        }

        private long id;
        private string email;
        private string firstname;
        private string lastname;
        private string address;
        private IHMRole role;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public IHMRole Role
        {
            get { return role; }
            set { role = value; }
        }

    }
}
