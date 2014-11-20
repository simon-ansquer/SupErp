using SupErp.IHM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupErp.IHM.ViewModels
{
    class MainWindowViewModel
    {
        #region Commands
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand;
            }
            set
            {
                _connectCommand = value;
            }
        }
        
        #endregion

        #region Command Handlers
        public MainWindowViewModel()
        {
            ConnectCommand = new RelayCommand(new Action<object>(Connect));
        }
        #endregion

        public void Connect(object obj)
        {
            
        }
    }
}
