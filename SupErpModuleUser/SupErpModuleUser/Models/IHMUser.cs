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
        private long id;
        private string email;
        private string firstname;
        private string lastname;
        private string password;
        private string address;
        private IHMRole role;
        private bool isNew;
        private string city;

        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged("City"); }
        }

        private string zipcode;

        public string Zipcode
        {
            get { return zipcode; }
            set { zipcode = value; OnPropertyChanged("Zipcode"); }
        }
        
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        public IHMUser()
        {
            isNew = true;
        }

        public IHMUser(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Address = user.Address;
            Role = user.Role != null ? user.Role.ToIHMRole() : null;
            isNew = false;
            Zipcode = user.Zip_code;
            City = user.City;
        }

        public long Id
        {
            get { return id; }
            set { 
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Email
        {
            get { return email; }
            set { 
                email = value;
                OnPropertyChanged("Email");    
            }
        }

        public string Firstname
        {
            get { return firstname; }
            set { 
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        public string Lastname
        {
            get { return lastname; }
            set { 
                lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        public string Address
        {
            get { return address; }
            set { 
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public IHMRole Role
        {
            get { return role; }
            set { 
                role = value;
                OnPropertyChanged("Role");
            }
        }

        public bool IsNew
        {
            get { return isNew; }
            set {
                isNew = value;
                OnPropertyChanged("IsNew");
            }
        }

    }
}
