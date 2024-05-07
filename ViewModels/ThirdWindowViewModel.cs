using ProjektTOWAM.BazaDanych;
using ProjektTOWAM.Models;
using ProjektTOWAM.Views;
using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Text;

namespace ProjektTOWAM.ViewModels
{
    public class ThirdWindowViewModel : KlasaBazowa
    {
        // czy można iśc do analizy
        public bool IdzDoAnalizy { get; set; }

        private DaneOsobowePacjent _nowyPacjent;
        public DaneOsobowePacjent NowyPacjent
        {
            get => _nowyPacjent;
            set
            {
                if (_nowyPacjent != value)
                {
                    _nowyPacjent = value;
                    OnPropertyChanged();
                }
            }
        }
        private WynikPacjenta _nowyWynikPacjenta;
        public WynikPacjenta NowyWynikPacjenta
        {
            get => _nowyWynikPacjenta;
            set
            {
                if (_nowyWynikPacjenta != value)
                {
                    _nowyWynikPacjenta = value;
                    OnPropertyChanged();
                }
            }
        }
        
        // komendy do przycisków
        public ICommand ZatwierdzCommand { get; set; }
        public ICommand ZapiszCommand { get; set; }

        public ThirdWindowViewModel()
        {
            Inicjalizacja();
        }

        private void Inicjalizacja()
        {
            InicjalizacjaKomend();

            NowyPacjent = new DaneOsobowePacjent();
            // ustawienie domyslnych wartosci
            NowyWynikPacjenta = new WynikPacjenta() { Sex = "0", Education = "1" };
        }

        private void InicjalizacjaKomend()
        {
            ZatwierdzCommand = new RelayCommand<object>(ExecZatwierdz);
            ZapiszCommand = new RelayCommand<object>(ExecZapisz);
        }

        private bool DodajPacjenta()
        {
            // jesli dane pole jest puste to wyswietla komunikat , że musi być podane
            if (string.IsNullOrWhiteSpace(NowyPacjent.Imie))
            {
                MessageBox.Show("Imię musi być podane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NowyPacjent.Nazwisko))
            {
                MessageBox.Show("Nazwisko musi być podane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NowyPacjent.Pesel))
            {
                MessageBox.Show("Pesel musi być podany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NowyPacjent.MiejsceUrodzenia))
            {
                MessageBox.Show("Miejsce urodzenia musi być podane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NowyPacjent.MiejsceZamieszkania))
            {
                MessageBox.Show("Miejsce zamieszkania musi być podane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NowyPacjent.NumerTelefonu))
            {
                MessageBox.Show("Numer telefonu musi być podany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NowyPacjent.EmailPacjenta))
            {
                MessageBox.Show("E-mail musi być podane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            // jeżeli wszystko jest spelnione to dodajemy do bazy
            App.Baza.DaneOsobowePacjenci.Add(NowyPacjent);
            
            return App.Baza.SaveChanges() > 0;
        }

         private bool DodajNowyWynik()
        {
            if(string.IsNullOrWhiteSpace(NowyWynikPacjenta.Sex))
            {
                MessageBox.Show("Sex musi być podane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if(string.IsNullOrWhiteSpace(NowyWynikPacjenta.Education))
            {
                MessageBox.Show("Education musi być podane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (DodajPacjenta())
            {
                // id jest zwórcone z bazy po dodaniu, nowy id przypisujemy do id pacjenta i dodajemy go do bazy
                NowyWynikPacjenta.IdPacjenta = NowyPacjent.Id;
                App.Baza.WynikiPacjenta.Add(NowyWynikPacjenta);
                
                return App.Baza.SaveChanges() > 0;
            }

            return false;
        }

        //metody do zatwierdzenia i zapisywania wyników
        private bool Zatwierdz()
        {
            return DodajNowyWynik();
        }

        private bool Zapisz()
        {
            IdzDoAnalizy = DodajNowyWynik();
            return IdzDoAnalizy;
        }

        private void ExecZatwierdz(object obj)
        {
            // sprawdzenie czy parametr jest typu ThirdWindow i Zatwierdzenie
            if (obj is ThirdWindow w && Zatwierdz())
                // jesli wszytsko jest  okej to zamykamy okno
                w.DialogResult = true;
        }

        private void ExecZapisz(object obj)
        {
            if (obj is ThirdWindow w && Zapisz())
                w.DialogResult = true;
        }
        
    }
}
