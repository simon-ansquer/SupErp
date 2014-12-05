﻿using SupErpModuleUser.UserService;
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
        private string address;
        private IHMRole role;
        private bool isNew;

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
            Role = user.Role.ToIHMRole();
            isNew = false;
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
                OnPropertyChanged("FirstName");
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
