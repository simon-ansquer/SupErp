using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using SupErp.IHM.Helpers;
using SupErp.IHM.Views;
using SupErp.Kernel;
using SupErp.Shared;

namespace SupErp.IHM.ViewModels
{
    class LoginPageViewModel
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
        public LoginPageViewModel()
        {
            ConnectCommandHandler = new RelayCommand(new Action<object>(ConnectCommand));
        }
        #endregion


        #region Commands

        public void ConnectCommand(object obj)
        {
            var passwordBox = obj as PasswordBox;
            if (passwordBox != null) password = passwordBox.Password;
            Connect();
        }
        #endregion



        #region Methodes

        public void Connect()
        {
            
            var user = WCFManager.UserServiceClient.Login(login, password);

            DllManager dllManager = new DllManager();
            IEnumerable<IMainMenu> mainMenus = dllManager.GetMainMenus(user.Role);
            MainWindow.MainFrame.Navigate(new MenuPage(mainMenus));
        }
        #endregion
    }
}
