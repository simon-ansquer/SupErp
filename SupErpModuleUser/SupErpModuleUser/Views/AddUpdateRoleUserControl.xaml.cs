using SupErp.Shared;
using SupErpModuleUser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupErpModuleUser
{
    /// <summary>
    /// Logique d'interaction pour AddRoleUserControl.xaml
    /// </summary>
    public partial class AddUpdateRoleUserControl : UserControl
    {
        public AddUpdateRoleUserControl()
        {
            InitializeComponent();
        }

        public AddUpdateRoleUserControl(AddUpdateRoleViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

       

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
                e.Column.Visibility = Visibility.Collapsed;
            if (e.Column.Header.ToString() == "IdRoleModule")
                e.Column.Visibility = Visibility.Collapsed;
            if (e.Column.Header.ToString() == "IsWritingSelected")
                e.Column.Visibility = Visibility.Collapsed;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            var g = maingrid.Children;
            var newSize = (sizeInfo.NewSize.Height * sizeInfo.NewSize.Width);
            foreach (UIElement el in g)
            {
                if (el.GetType() == typeof(TextBlock))
                    ((TextBlock)el).FontSize = newSize / 50000;
                if (el.GetType() == typeof (TextBox))
                {
                    ((TextBox)el).FontSize = newSize/50000;
                    ((TextBox)el).Height = newSize / 35000;
                }
            }
            base.OnRenderSizeChanged(sizeInfo);
        }
    }
}
