using ProjektTOWAM.BazaDanych;
using ProjektTOWAM.Models;
using ProjektTOWAM.Views;
using System.Windows.Input;
using System.IO;
using System.Text;
using System;
using System.Windows;
using System.Linq;

namespace ProjektTOWAM.ViewModels
{
    public class FinalWindowViewModel : KlasaBazowa
    {
        // przekazujemy pacjenta ( imie i nazwisko, które wyświetla się na górze)
        private DaneOsobowePacjent _pacjent;
        private bool _czyPacjentUstawiony = false;

        public DaneOsobowePacjent Pacjent
        {
            get => _pacjent;
            set
            {
                if (!_czyPacjentUstawiony)
                {
                    _pacjent = value;
                    if (_pacjent != null)
                    {
                        Diagnozaa = ReadCsv(_pacjent.Id);
                    }
                    _czyPacjentUstawiony = true;
                    
                    OnPropertyChanged();
                }
            }
        }
        private Lekarz _zalogowanyLekarz;
        private bool _czyLekarzUstawiony = false;
        public Lekarz ZalogowanyLekarz
        {
            get => _zalogowanyLekarz;
            set
            {
                if (!_czyLekarzUstawiony)
                {
                    _zalogowanyLekarz = value;
                    
                    _czyPacjentUstawiony = true;

                    OnPropertyChanged();
                }
            }
        }


        // diagnoza 
        private DiagnozaPacjenta _diagnoza;
        public DiagnozaPacjenta Diagnoza
        {
            get => _diagnoza;
            set
            {
                if (_diagnoza != value)
                {
                    _diagnoza = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _diagnozaa;
        public string Diagnozaa
        {
            get => _diagnozaa;
            set
            {
                
                if (_diagnozaa != value)
                {
                    _diagnozaa = value;
                    OnPropertyChanged();
                }
                
                
            }
        }

        private string _komentarzLekarzaTextBox;
        public string KomentarzLekarzaTextBox
        {
            get => _komentarzLekarzaTextBox;
            set
            {
                if (_komentarzLekarzaTextBox != value)
                {
                    _komentarzLekarzaTextBox = value;
                    OnPropertyChanged(nameof(KomentarzLekarzaTextBox));
                }
            }
        }



        // komendy Zapisz i Zamknij dla przycisków
        public ICommand ZapiszCommand { get; set; }

        public ICommand ZamknijCommand { get; set; }

        // w konstruktorze wywołanie metody InicjalizacjaKomend()
        public FinalWindowViewModel()
        {
            Inicjalizacja();
            

        }
        private void Inicjalizacja()
        {
            InicjalizacjaKomend();
            
            
        }

        private void InicjalizacjaKomend()
        {
            // inicjalizujemy komendy
            ZapiszCommand = new RelayCommand<object>(ExecZapisz);//, x => !string.IsNullOrWhiteSpace(KomentarzLekarzaTextBox));
            ZamknijCommand = new RelayCommand<object>(ExecZamknij);
        }

        

        
        private void ExecZapisz(object obj)
        {
            if (obj is FinalWindow w )
            {

                var uzytkownik = App.Baza.Lekarze.FirstOrDefault(u => u.Email == ZalogowanyLekarz.Email && u.Haslo == ZalogowanyLekarz.Haslo);
                Diagnoza = new DiagnozaPacjenta
                {
                    IdPacjenta = Pacjent.Id,
                    IdLekarza = uzytkownik.Id,
                    Diagnoza = ReadCsv(Pacjent.Id),
                    KomentarzLekarza = KomentarzLekarzaTextBox

                };
                if (Diagnoza != null)
                {
                    App.Baza.DiagnozaPacjentow.Add(Diagnoza);
                }
                w.DialogResult = true;
            }
        }
        
        private void ExecZamknij(object obj)
        {          
            if (obj is FinalWindow w)

                w.DialogResult = true;
        }
    }
}
