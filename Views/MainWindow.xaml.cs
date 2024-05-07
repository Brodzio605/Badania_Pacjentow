using ProjektTOWAM.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjektTOWAM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm && sender is PasswordBox pb)
                vm.ZalogowanyLekarz.Haslo = pb.Password;
        }
    }

}



