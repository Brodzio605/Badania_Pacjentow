using Microsoft.IdentityModel.Tokens;
using ProjektTOWAM.BazaDanych;
using ProjektTOWAM.Models;
using ProjektTOWAM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjektTOWAM.ViewModels
{
    public class MainWindowViewModel : KlasaBazowa
    {
        // to odpowiada za widoczność dla dwóch gridów
        private Visibility _pokazGridLogowania;
        public Visibility PokazGridLogowania
        {
            get => _pokazGridLogowania;
            set
            {
                if (_pokazGridLogowania != value)
                {
                    _pokazGridLogowania = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _pokazGridGlowny;
        public Visibility PokazGridGlowny
        {
            get => _pokazGridGlowny;
            set
            {
                if (_pokazGridGlowny != value)
                {
                    _pokazGridGlowny = value;
                    OnPropertyChanged();
                }
            }
        }
        // informacja o zalogowanym lekarzu
        public Lekarz ZalogowanyLekarz { get; set; }

        // komendy do poszczególnych przycisków
        public ICommand LoginCommand { get; set; }
        public ICommand DodajPacjentaCommand { get; set; }
        public ICommand UsunPacjentaCommand { get; set; }
        public ICommand OtworzBazePacjentowCommand { get; set; }


        public MainWindowViewModel()
        {
            Inicjalizacja();
        }

        private void Inicjalizacja()
        {
            InicjalizacjaKomend();

            // ustawienie który grid ma być widoczny, a który ukryty
            PokazGridLogowania = Visibility.Visible;
            PokazGridGlowny = Visibility.Collapsed;
            // utowrzenie lekarza
            ZalogowanyLekarz = new Lekarz();
        }

        private void InicjalizacjaKomend()
        {
            LoginCommand = new RelayCommand<object>(ExecLogin, x => CanLogIn());
            DodajPacjentaCommand = new RelayCommand<object>(ExecDodajPacjenta);
            UsunPacjentaCommand = new RelayCommand<object>(ExecUsunPacjenta);
            OtworzBazePacjentowCommand = new RelayCommand<object>(ExecOtworzBazePacjentow);
        }

        private bool CanLogIn()
        {
            // sprawdzenie czy zalogowany lekarz jest różny od null, czy jest podany email i hasło 
            return ZalogowanyLekarz != null && !string.IsNullOrEmpty(ZalogowanyLekarz.Email) && !string.IsNullOrEmpty(ZalogowanyLekarz.Haslo);
        }


        private void ExecLogin(object obj)
        {
            if (CanLogIn())
            {
                // wyszukujemy uzytkownika, który ma taki login i hasło 
                var uzytkownik = App.Baza.Lekarze.FirstOrDefault(u => u.Email == ZalogowanyLekarz.Email && u.Haslo == ZalogowanyLekarz.Haslo);
                if (uzytkownik != null)
                {
                    // jeśli znajdziemy takiego uzytkownika to zmieniamy widocznosc,
                    // wyswietlamy grida głównego z przyskamy, a ukrywamy tego z logowaniem
                    PokazGridGlowny = Visibility.Visible;
                    PokazGridLogowania = Visibility.Collapsed;
                }
                else
                {
                    // jesli logowanie nie wyjdzie to wyswietla ze sa bledne dane
                    MessageBox.Show("Błędne dane logowania!", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Wprowadź nazwę użytkownika i hasło.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExecDodajPacjenta(object obj)
        {
            // utworzenie trzeciego okna
            ThirdWindow window = new ThirdWindow();
            if (window.ShowDialog().Value)
            {
                //wyswietlenie kolejnego okna
                FourthWindow fourthwindow = new FourthWindow();
                //do zmiennej przypisujemy DataContext jako ViewModel
                FourthWindowViewModel cont = fourthwindow.DataContext as FourthWindowViewModel;
                // ustawiamy ostatniego pacjenta z listy
                cont.WybranyPacjent = cont.DaneOsobowePacjentow.LastOrDefault();
                cont.ZalogowanyLekarz = ZalogowanyLekarz;
                fourthwindow.ShowDialog();
            }
        }
        // usuwanie pacjenta
        private void ExecUsunPacjenta(object obj)
        {
            // wyswietlenie okna
            FifthWindow window = new FifthWindow();
            window.ShowDialog();
        }

        private void ExecOtworzBazePacjentow(object obj)
        {
            // okno do wyświetlenia bazy pacjentów
            FourthWindow window = new FourthWindow();
            FourthWindowViewModel cont = window.DataContext as FourthWindowViewModel;
            cont.ZalogowanyLekarz = ZalogowanyLekarz;
            window.ShowDialog();
        }
    }
}