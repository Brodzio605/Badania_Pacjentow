using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ProjektTOWAM.BazaDanych
{
    public class AppDbContext : DbContext
    {
        private readonly string _connString = ConfigurationManager
                                            .ConnectionStrings["ConnectionString"].ConnectionString;
        // odwołanie sie do tabeli lekarzy, danych osobowych pacjentów i wyników Pacjenta ( pozwala to usować, dodwawac i edytować dane)
        public DbSet<Lekarz> Lekarze { get; set; }
        public DbSet<DaneOsobowePacjent> DaneOsobowePacjenci { get; set; }
        public DbSet<WynikPacjenta> WynikiPacjenta { get; set; }
        public DbSet<DiagnozaPacjenta> DiagnozaPacjentow { get; set; }

        // tworzenie bazy jeśli nie istnieje
        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        // uzycie SQL serwera i connectionString, który jest odczytywany z App.config
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connString);
        }
    }
}
