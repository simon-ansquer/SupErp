using SupErp.IHM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupErp.IHM.ServiceReference1;

namespace SupErp.IHM.ViewModels
{
    class MainWindowViewModel
    {
        

        #region inputs
        /// <summary>
        /// The login
        /// </summary>
        private string login;

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>
        public string Login
        {
            get { return this.login; }
            set
            {
                if (!string.Equals(this.login, value))
                {
                    this.login = value;
                }
            }
        }

        /// <summary>
        /// The password
        /// </summary>
        private string password;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get { return this.password; }
            set
            {
                if (!string.Equals(this.password, value))
                {
                    this.password = value;
                }
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// The connect command
        /// </summary>
        private ICommand connectCommandHandler;
        /// <summary>
        /// Gets or sets the connect command.
        /// </summary>
        /// <value>
        /// The connect command.
        /// </value>
        public ICommand ConnectCommandHandler
        {
            get
            {
                return connectCommandHandler;
            }
            set
            {
                connectCommandHandler = value;
            }
        }
        
        #endregion

        #region Command Handlers
        public MainWindowViewModel()
        {
            ConnectCommandHandler = new RelayCommand(new Action<object>(ConnectCommand));
        }
        #endregion


        #region Commands
        public void ConnectCommand(object obj) {Connect();}
        #endregion



        #region Methodes

        public User Connect()
        {
            User user = WCFManager.UserServiceClient.Login(login, password);
            return user;
        }
        #endregion
    }
}
