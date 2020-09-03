using Diary.ViewModels;
using MahApps.Metro.Controls;


namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    { 
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModels();

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
