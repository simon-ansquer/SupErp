using System.Threading;
using System.Windows.Markup;
using WpfControlLibrarySalaire.Interfaces;

namespace WpfControlLibrarySalaire.Views
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class EmployeesList : IMainMenu
    {
        public EmployeesList()
        {
            InitializeComponent();
            Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
        }

        public string MenuName
        {
            get { return "Salaires"; }
        }

    }
}
