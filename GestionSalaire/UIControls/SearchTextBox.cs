using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace UIControls
{
    /// <summary>
    /// Suivez les étapes 1a ou 1b puis 2 pour utiliser ce contrôle personnalisé dans un fichier XAML.
    ///
    /// Étape 1a) Utilisation de ce contrôle personnalisé dans un fichier XAML qui existe dans le projet actif.
    /// Ajoutez cet attribut XmlNamespace à l'élément racine du fichier de balisage où il doit 
    /// être utilisé :
    ///
    ///     xmlns:MyNamespace="clr-namespace:UIControls"
    ///
    ///
    /// Étape 1b) Utilisation de ce contrôle personnalisé dans un fichier XAML qui existe dans un autre projet.
    /// Ajoutez cet attribut XmlNamespace à l'élément racine du fichier de balisage où il doit 
    /// être utilisé :
    ///
    ///     xmlns:MyNamespace="clr-namespace:UIControls;assembly=UIControls"
    ///
    /// Vous devrez également ajouter une référence du projet contenant le fichier XAML
    /// à ce projet et régénérer pour éviter des erreurs de compilation :
    ///
    ///     Cliquez avec le bouton droit sur le projet cible dans l'Explorateur de solutions, puis sur
    ///     "Ajouter une référence"->"Projets"->[Sélectionnez ce projet]
    ///
    ///
    /// Étape 2)
    /// Utilisez à présent votre contrôle dans le fichier XAML.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public enum SearchMode
    {

        Instant,
        Delayed,
    }

    public class SearchTextBox : TextBox
    {

        public static DependencyProperty LabelTextProperty =
            DependencyProperty.Register(
                "LabelText",
                typeof(string),
                typeof(SearchTextBox));

        public static DependencyProperty LabelTextColorProperty =
            DependencyProperty.Register(
                "LabelTextColor",
                typeof(Brush),
                typeof(SearchTextBox));

        public static DependencyProperty SearchModeProperty =
            DependencyProperty.Register(
                "SearchMode",
                typeof(SearchMode),
                typeof(SearchTextBox),
                new PropertyMetadata(SearchMode.Instant));

        private static DependencyPropertyKey HasTextPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "HasText",
                typeof(bool),
                typeof(SearchTextBox),
                new PropertyMetadata());
        public static DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        private static DependencyPropertyKey IsMouseLeftButtonDownPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsMouseLeftButtonDown",
                typeof(bool),
                typeof(SearchTextBox),
                new PropertyMetadata());
        public static DependencyProperty IsMouseLeftButtonDownProperty = IsMouseLeftButtonDownPropertyKey.DependencyProperty;

        public static DependencyProperty SearchEventTimeDelayProperty =
            DependencyProperty.Register(
                "SearchEventTimeDelay",
                typeof(Duration),
                typeof(SearchTextBox),
                new FrameworkPropertyMetadata(
                    new Duration(new TimeSpan(0, 0, 0, 0, 500)),
                    new PropertyChangedCallback(OnSearchEventTimeDelayChanged)));

        public static readonly RoutedEvent SearchEvent =
            EventManager.RegisterRoutedEvent(
                "Search",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SearchTextBox));

        static SearchTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SearchTextBox),
                new FrameworkPropertyMetadata(typeof(SearchTextBox)));
        }

        private DispatcherTimer searchEventDelayTimer;

        public SearchTextBox()
        {
            searchEventDelayTimer = new DispatcherTimer {Interval = SearchEventTimeDelay.TimeSpan};
            searchEventDelayTimer.Tick += new EventHandler(OnSeachEventDelayTimerTick);
        }

        void OnSeachEventDelayTimerTick(object o, EventArgs e)
        {
            searchEventDelayTimer.Stop();
            RaiseSearchEvent();
        }

        static void OnSearchEventTimeDelayChanged(
            DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var stb = o as SearchTextBox;
            if (stb != null)
            {
                stb.searchEventDelayTimer.Interval = ((Duration)e.NewValue).TimeSpan;
                stb.searchEventDelayTimer.Stop();
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            HasText = Text.Length != 0;

            if (SearchMode == SearchMode.Instant)
            {
                searchEventDelayTimer.Stop();
                searchEventDelayTimer.Start();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var iconBorder = GetTemplateChild("PART_SearchIconBorder") as Border;
            if (iconBorder != null)
            {
                iconBorder.MouseLeftButtonDown += new MouseButtonEventHandler(IconBorder_MouseLeftButtonDown);
                iconBorder.MouseLeftButtonUp += new MouseButtonEventHandler(IconBorder_MouseLeftButtonUp);
                iconBorder.MouseLeave += new MouseEventHandler(IconBorder_MouseLeave);
            }
        }

        private void IconBorder_MouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            IsMouseLeftButtonDown = true;
        }

        private void IconBorder_MouseLeftButtonUp(object obj, MouseButtonEventArgs e)
        {
            if (!IsMouseLeftButtonDown) return;

            if (HasText && SearchMode == SearchMode.Instant)
            {
                this.Text = "";
            }

            if (HasText && SearchMode == SearchMode.Delayed)
            {
                RaiseSearchEvent();
            }

            IsMouseLeftButtonDown = false;
        }

        private void IconBorder_MouseLeave(object obj, MouseEventArgs e)
        {
            IsMouseLeftButtonDown = false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape && SearchMode == SearchMode.Instant)
            {
                this.Text = "";
            }
            else if ((e.Key == Key.Return || e.Key == Key.Enter) &&
                SearchMode == SearchMode.Delayed)
            {
                RaiseSearchEvent();
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void RaiseSearchEvent()
        {
            var args = new RoutedEventArgs(SearchEvent);
            RaiseEvent(args);
        }

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public Brush LabelTextColor
        {
            get { return (Brush)GetValue(LabelTextColorProperty); }
            set { SetValue(LabelTextColorProperty, value); }
        }

        public SearchMode SearchMode
        {
            get { return (SearchMode)GetValue(SearchModeProperty); }
            set { SetValue(SearchModeProperty, value); }
        }

        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
            private set { SetValue(HasTextPropertyKey, value); }
        }

        public Duration SearchEventTimeDelay
        {
            get { return (Duration)GetValue(SearchEventTimeDelayProperty); }
            set { SetValue(SearchEventTimeDelayProperty, value); }
        }

        public bool IsMouseLeftButtonDown
        {
            get { return (bool)GetValue(IsMouseLeftButtonDownProperty); }
            private set { SetValue(IsMouseLeftButtonDownPropertyKey, value); }
        }

        public event RoutedEventHandler Search
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }
    }
}
