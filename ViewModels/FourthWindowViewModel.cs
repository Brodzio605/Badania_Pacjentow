using ProjektTOWAM.BazaDanych;
using ProjektTOWAM.Models;
using ProjektTOWAM.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProjektTOWAM.ViewModels
{
    public class FourthWindowViewModel : KlasaBazowa
    {
        // dane pacjenta ( imie, nazwisko, e-mail, które wyswietlaa sie w górnej czesci okna)
        private ObservableCollection<DaneOsobowePacjent> _daneOsobowePacjentow;

        public ObservableCollection<DaneOsobowePacjent> DaneOsobowePacjentow
        {
            get => _daneOsobowePacjentow;
            set
            {
                if (_daneOsobowePacjentow != value)
                {
                    _daneOsobowePacjentow = value;
                    OnPropertyChanged();
                }
            }
        }
        // dane wybranego pacjenta, które wyświetlaja się w edycji pacjenta
        private DaneOsobowePacjent _wybranyPacjent;

        public DaneOsobowePacjent WybranyPacjent
        {
            get => _wybranyPacjent;
            set
            {
                if (_wybranyPacjent != value)
                {
                    // jesli zmieniamy zaznaczenie to tworzy się nowy pacjent na podstawie danych z wybranego pacjenta
                    _wybranyPacjent = value;
                    // jezeli zmienimy dane, to dotyczy tylko edytowalnego pacjenta, a nie będzie modyfikować pacjenta na liście
                    EdytowanyPacjent = new DaneOsobowePacjent(_wybranyPacjent);
                    // druga zakładka wyniki pacjenta 
                    EdytowaneWynikiPacjenta = WezWynikPacjentaPoId(_wybranyPacjent.Id);
                    OnPropertyChanged();
                }
            }
        }
        public Lekarz ZalogowanyLekarz { get; set; }

        private DaneOsobowePacjent _edytowanyPacjent;

        public DaneOsobowePacjent EdytowanyPacjent
        {
            get => _edytowanyPacjent;
            set
            {
                if (_edytowanyPacjent != value)
                {
                    _edytowanyPacjent = value;
                    OnPropertyChanged();
                }
            }
        }

        private WynikPacjenta _edytowaneWynikiPacjenta;

        public WynikPacjenta EdytowaneWynikiPacjenta
        {
            get => _edytowaneWynikiPacjenta;
            set
            {
                if (_edytowaneWynikiPacjenta != value)
                {
                    _edytowaneWynikiPacjenta = value;
                    OnPropertyChanged();
                }
            }
        }
        

        public ICommand WynikiCommand { get; set; }

        public ICommand ZapiszCommand { get; set; }

        public FourthWindowViewModel()
        {
            Inicjalizacja();
        }

        private void Inicjalizacja()
        {
            InicjalizacjaKomend();
            WczytajPacjentow();
        }

        private void InicjalizacjaKomend()
        {
            WynikiCommand = new RelayCommand<object>(ExecWyniki);
            ZapiszCommand = new RelayCommand<object>(ExecZapisz);
        }

        private bool Aktualizuj()
        {
            // wyszukiwianie pacjenta po id, aktualizacja pobranego pacjenta 
            var pacjent = App.Baza.DaneOsobowePacjenci.FirstOrDefault(x => x.Id == WybranyPacjent.Id);
            if (pacjent != null)
            {
                // aktualizacja danych pobranego pacjenta
                pacjent.Aktualizuj(EdytowanyPacjent);
                // wyszukujemy wyniki i też je aktualizujemy
                var wyniki = App.Baza.WynikiPacjenta.FirstOrDefault(x => x.IdPacjenta == pacjent.Id);
                // jeśli wyniki będą różne od nulla to wywołujemy metodę aktualizuj
                wyniki?.Aktualizuj(EdytowaneWynikiPacjenta);
                // aktualizacja pacjenta i wyników
                App.Baza.Update(pacjent);
                App.Baza.Update(wyniki);
                // zapisywanie wyników 
                SaveToCsv(wyniki);
                // zapis do bazy, jeśli się uda to większe od 0
                return App.Baza.SaveChanges() > 0;
            }
            // jesli nie znajdzie pacjenta, to aktualizacja sie nie uda 
            return false;
        }

        private WynikPacjenta WezWynikPacjentaPoId(int id)
        {
            // odczytywanie wyników z bazy, taki wynik, który odpowiadaja id obecnie zaznaczonego pacjenta
            return App.Baza.WynikiPacjenta.FirstOrDefault(x => x.IdPacjenta == id);
        }

        private void WczytajPacjentow()
        {
            // inicjalizacja kolekcji, w parametrze przekazujemy cala liste, zaczytujemy wszytskuch pacjentów
            DaneOsobowePacjentow = new ObservableCollection<DaneOsobowePacjent>(App.Baza.DaneOsobowePacjenci.ToList());
        }

        // wyniki wczytywane gdy klikniemy przycisk wyniki
        private void ExecWyniki(object obj)
        {
            // Tworzenie nowego okna
            FinalWindow fw = new FinalWindow();

            // Przypisanie do zmiennej ViewModel
            FinalWindowViewModel vm = fw.DataContext as FinalWindowViewModel;

            // Odwołujemy się do wybranego pacjenta, żeby wyświetlić na górze okna imię i nazwisko
            vm.Pacjent = WybranyPacjent;

            // Przekazanie pacjenta i lekarza do konstruktora ViewModel
            vm.ZalogowanyLekarz = ZalogowanyLekarz;

            string y=ReadCsv(WybranyPacjent.Id);
            // Wyświetlanie okna
            if (y!=null)
            {
              fw.ShowDialog();
            }
            
        }

        private void ExecZapisz(object obj)
        {
            // sprawdzenie czy parametr jest typu FourthWindow i czy aktualizacja się udała
            if (obj is FourthWindow w && Aktualizuj())
                // jesli wszystko sie uda to zamykamy okno
                w.DialogResult = true;
        }
    }
}







