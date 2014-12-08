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
using SupErp.IHM.Helpers;
using SupErp.IHM.Models;
using SupErp.IHM.ViewModels;
using SupErp.Shared;
using System.Linq;

namespace SupErp.IHM.Views
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public IEnumerable<IMainMenu> MainMenus { get; set; }
        public List<Grid> SubMenus { get; set; }

        public MenuPage(IEnumerable<IMainMenu> mainMenus)
        {
            InitializeComponent();

            MainMenus = mainMenus;
            SubMenus = new List<Grid>();
            ScreenHeight = StaticParams.ScreenHeight;
            ScreenWidth = StaticParams.ScreenWidth;
            Menus.ItemsSource = MainMenus;

            SetTextSize();
        }

        private void SetTextSize()
        {
            Logo.FontSize = ScreenHeight / 20;
            LogOut.FontSize = ScreenHeight / 40;
            LogOutImage.Width = ScreenHeight / 25;
            LogOutImage.Height = ScreenHeight / 25;

            //Connexion.FontSize = ScreenHeight / 35;

            //LoginTbl.FontSize = ScreenHeight / 45;
            //LoginTbx.Height = ScreenHeight / 30;
            //LoginTbx.FontSize = ScreenHeight / 50;
            //LoginTbx.Focus();

            //PassTbl.FontSize = ScreenHeight / 45;
            //PassTbx.Height = ScreenHeight / 30;
            //PassTbx.FontSize = ScreenHeight / 50;

            //Connect.Height = ScreenHeight / 25;
            //Connect.Width = (ScreenWidth * 0.4) * 0.3;
            //Connect.FontSize = ScreenHeight / 50;
        }

        private void LogOutPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            LogOut.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
        }
        private void LogOutPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            LogOut.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void LogOutClicked(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrame.Navigate(new LoginPage());
        }

        private void MenuItem_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock t = (sender as TextBlock);

            if (t != null)
            {
                t.FontSize = ScreenHeight / 38;
                t.Height = ScreenHeight / 35;
            }
        }

        private void MainMenuClicked(object sender, SelectionChangedEventArgs e)
        {
            IMainMenu item = (Menus.SelectedItem as IMainMenu);
            ClearSubMenus(false);

            if (item != null)
            {
                if (item.SubMenus != null && item.SubMenus.Count > 0)
                {
                    ListBoxItem listBoxItem = (Menus.ItemContainerGenerator.ContainerFromIndex(((ListBox)sender).SelectedIndex) as ListBoxItem);
                    Point position = listBoxItem.TransformToVisual((Visual) (Menus.Parent)).Transform(new Point(listBoxItem.ActualWidth, 0));
                    GenerateSubMenus(item.SubMenus, position, false);
                }
                else
                {
                    RightTape.Children.Clear();
                    RightTape.Children.Add(item as UserControl);
                }
            }
        }

        private void GenerateSubMenus(IEnumerable<ISubMenu> submenus, Point position, bool dark)
        {
            Grid grid = new Grid();
            grid.Name = "SubMenu";
            grid.Margin = new Thickness(position.X, position.Y, 0, 0);
            grid.Background = new SolidColorBrush(dark ? Color.FromArgb(255, 67, 136, 204) : Color.FromArgb(255, 51, 122, 204));
            grid.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            grid.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ListBox listBox = new ListBox();
            listBox.Background = new SolidColorBrush(Colors.Transparent);
            listBox.BorderThickness = new Thickness(0);
            listBox.SelectionChanged += listBox_SelectionChanged;

            Binding binding = new Binding();
            binding.Path = new PropertyPath("SubMenuName");

            var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFactory.SetValue(TextBlock.TextProperty, binding);
            textBlockFactory.SetValue(TextBlock.ForegroundProperty, Brushes.White);
            textBlockFactory.SetValue(TextBlock.FontSizeProperty, ScreenHeight / 38);
            textBlockFactory.SetValue(TextBlock.HeightProperty, ScreenHeight / 35);
            textBlockFactory.SetValue(TextBlock.MarginProperty, new Thickness(0,0,0,10));

            var stackPanel = new FrameworkElementFactory(typeof(StackPanel));
            stackPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            stackPanel.AppendChild(textBlockFactory);

            DataTemplate dataTemplate = new DataTemplate();
            dataTemplate.VisualTree = stackPanel;

            listBox.ItemTemplate = dataTemplate;
            listBox.ItemsSource = submenus;          
            
            grid.Children.Add(listBox);
            SubMenus.Add(grid);
            MainGrid.Children.Add(grid);
            Grid.SetColumnSpan(grid, 2);
        }

        void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ISubMenu item = (((ListBox)sender).SelectedItem as ISubMenu);

            if (item.SubMenus != null && item.SubMenus.Count > 0)
            {
                ListBoxItem listBoxItem = (((ListBox)sender).ItemContainerGenerator.ContainerFromIndex(((ListBox)sender).SelectedIndex) as ListBoxItem);
                Point position = listBoxItem.TransformToVisual((Visual)(Menus.Parent)).Transform(new Point(listBoxItem.ActualWidth, 0));
                GenerateSubMenus(item.SubMenus, position, ((SolidColorBrush)(((Grid)(((ListBox)sender).Parent)).Background)).Color.Equals(Color.FromArgb(255, 51, 122, 204)));
            }
            else
            {
                RightTape.Children.Clear();
                RightTape.Children.Add(item as UserControl);
            }
        }

        private void MainGridClicked(object sender, MouseButtonEventArgs e)
        {
            ClearSubMenus(true);
        }

        private void ClearSubMenus(bool unselectAll)
        {
            if(unselectAll)
                Menus.UnselectAll();

            for (int i = 0; i < SubMenus.Count(); i++)
                MainGrid.Children.Remove(SubMenus[i]);

            SubMenus.Clear();
        }
    }
}
