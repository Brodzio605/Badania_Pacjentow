using ProjektTOWAM.BazaDanych;
using ProjektTOWAM.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ProjektTOWAM.ViewModels
{
    public class FifthWindowViewModel : KlasaBazowa
    {

        // lista pacjentów, ObservableCollection - > wyłapuje kiedy pacjenci zostali usunięci/dodani
        private ObservableCollection<DaneOsobowePacjent> _pacjenci;
        public ObservableCollection<DaneOsobowePacjent> Pacjenci
        {
            get => _pacjenci;
            set
            {
                if (_pacjenci != value)
                {
                    _pacjenci = value;
                    OnPropertyChanged();
                }
            }
        }


        private DaneOsobowePacjent _wybranyPacjent;
        public DaneOsobowePacjent WybranyPacjent
        {
            get => _wybranyPacjent;
            set
            {
                if (_wybranyPacjent != value)
                {
                    _wybranyPacjent = value;
                    OnPropertyChanged();
                }
            }
        }
        // komenda do usuwania podpięta pod przycisk

        public ICommand UsunCommand { get; set; }

        // w konstruktorze wywołanie metodey inicjalizacji
        public FifthWindowViewModel()
        {
            Inicjalizacja();
        }

        private void Inicjalizacja()
        {
            // wywołanie metody InicjalizacjaKomend
            InicjalizacjaKomend();

            Pacjenci = new ObservableCollection<DaneOsobowePacjent>(App.Baza.DaneOsobowePacjenci.ToList());
            //WybranyPacjent = Pacjenci.FirstOrDefault();
        }

        private void InicjalizacjaKomend()
        {
            // w parametrze przekazany obiekt typu object, nazwa metody RxecUsun wywołołana podczas kliknięcia, spełniony warunek-> pacjent nie może być nullem
            UsunCommand = new RelayCommand<object>(ExecUsun, x => WybranyPacjent != null);
        }

        private bool Usun()
        {
            // usuwamy wybranego pacjenta z bazy
            App.Baza.Remove(WybranyPacjent);
            // z bazy wszytskich wyników wyszukujemy pacjenta o id pacjenta, którego wybraliśmy
            var wyniki = App.Baza.WynikiPacjenta.FirstOrDefault(x => x.IdPacjenta == WybranyPacjent.Id);
            // usuwamy wszytskie wyniki pacjenta 
            App.Baza.Remove(wyniki);
            // jesli zapiszemy i wynik będzie większy od 0 to znaczy, że zapisało
            if (App.Baza.SaveChanges() > 0)
            {
                // z listy pacjentów wyświetlonych klasujemy wybranego
                Pacjenci.Remove(WybranyPacjent);
                // zaznaczamy ostatniego pacjenta z listy
                WybranyPacjent = Pacjenci.LastOrDefault();
                return true;
            }

            return false;
        }

        private void ExecUsun(object obj)
        {
            // przy klasowaniu wyświetla się MessageBox z pytaniem czy na pewno chce usunąć, przyciski tak/nie
            var dialog = MessageBox.Show($"Czy na pewno usunąć {WybranyPacjent.ImieNazwiskoEmail}?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            // jesli klikniemy tak to wywołana zostaje metoda Usuń
            if (dialog == MessageBoxResult.Yes)
                Usun();
        }
    }
}
