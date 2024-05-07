using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTOWAM.BazaDanych
{
    public class Lekarz
    {
        [Required]
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public DateTime DataZatrudnienia { get; set; }
        public string Stopien { get; set; }
    }
}
