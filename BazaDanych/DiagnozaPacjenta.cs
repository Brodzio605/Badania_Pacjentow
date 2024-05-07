using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTOWAM.BazaDanych
{
    public class DiagnozaPacjenta
    {
        [Required]
        public int Id { get; set; }
        public int IdPacjenta { get; set; }
        public int IdLekarza { get; set; }
        public string? Diagnoza { get; set; }
        public string? KomentarzLekarza {get; set; }
    }
}
