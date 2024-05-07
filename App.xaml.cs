using Microsoft.EntityFrameworkCore;
using ProjektTOWAM.BazaDanych;
using System.Windows;

namespace ProjektTOWAM
{

    public partial class App : Application
    {
        public static AppDbContext Baza { get; set; }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Baza = new AppDbContext();
        }
    }
}
