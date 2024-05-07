using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektTOWAM.BazaDanych
{
    // wskazywanie na tabele w bazie danych DaneOsobowePacjenta
    public class DaneOsobowePacjent
    {
        // Required oznacza żeby Id było obowiązkowe do nadania
        [Required]
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string MiejsceUrodzenia { get; set; }
        public string MiejsceZamieszkania { get; set; }
        public string NumerTelefonu { get; set; }
        public string EmailPacjenta { get; set; }

        // ta właściwość nie jest brana pod uwagę, ona nie jest mapowana,
        // pozwala wyswietlic trzy informacje na raz Imie, Nazwisko i Email Pacjenta

        [NotMapped]
        public string ImieNazwiskoEmail => $"{Imie} {Nazwisko} ({EmailPacjenta})";

        // konstruktory
        public DaneOsobowePacjent() { }

        // konstruktor przeciążony do aktualizacji pacjenta
        // na podstawie wybranego pacjenta, tworzymy nowego z takimi samymi wartościami pacjenta którego zaznaczyliśmy
        // dzięki temu w edycji będziemy mogli zmieniać dane pacjenta, a ten zaznaczony pozostanie bez zmian i będzie można anulować
        public DaneOsobowePacjent(DaneOsobowePacjent pacjent) : this()
        {
            Aktualizuj(pacjent);
        }

        public void Aktualizuj(DaneOsobowePacjent pacjent)
        {
            // sprawdzenie czy pacjent jest nulem,jesli tak to wychodzi, jeśli nie to przypisuje wszytsko
            // jesli by sie tego nie zrobiło to poniżej odwoływalibyśmy się do pacjenta który jest nulem i wywaliłoby apke
            if (pacjent == null)
                return;

            Imie = pacjent.Imie;
            Nazwisko = pacjent.Nazwisko;
            Pesel = pacjent.Pesel;
            DataUrodzenia = pacjent.DataUrodzenia;
            MiejsceUrodzenia = pacjent.MiejsceUrodzenia;
            MiejsceZamieszkania = pacjent.MiejsceZamieszkania;
            NumerTelefonu = pacjent.NumerTelefonu;
            EmailPacjenta = pacjent.EmailPacjenta;
        }
    }
}