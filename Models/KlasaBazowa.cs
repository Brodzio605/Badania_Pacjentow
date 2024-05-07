using ProjektTOWAM.BazaDanych;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektTOWAM.Models
{
    //klasa abstarkcjyjna -> nie można utworzyć instancji tej klasy, interfejs pozwala na odświeżanie właściwości podpiętych do kontrolek
    public abstract class KlasaBazowa : INotifyPropertyChanged
    {
        // odświeżanie własciwości
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SaveToCsv(WynikPacjenta wynik)
        {
            string desiredFilePath = "...\\Projekt Towam\\ostateczny\\WynikiPacjentów";
            string csvFileName = $"wynik_pacjenta_{wynik.IdPacjenta}.csv";
            string csvFilePath = Path.Combine(desiredFilePath, csvFileName);

            bool fileExists = File.Exists(csvFilePath);
            using (StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Diabetes_binary,HighBP,HighChol,CholCheck,BMI,Smoker,Stroke,HeartDiseaseorAttack,PhysActivity,Fruits,Veggies,HvyAlcoholConsump,AnyHealthcare,NoDocbcCost,GenHlth,MentHlth,PhysHlth,DiffWalk,Sex,Age,Education,Income");

                
                sw.WriteLine($"{wynik.Diabetes012},{wynik.HighBP},{wynik.CholCheck},{wynik.BMI},{wynik.Smoker},{wynik.Stroke},{wynik.HeartDiseaseorAttack},{wynik.PhysActivity},{wynik.Fruits},{wynik.Veggies},{wynik.HvyAlcoholConsump},{wynik.AnyHealthcare},{wynik.NoDocbcCost},{wynik.GenHlth},{wynik.MentHlth},{wynik.PhysHlth},{wynik.DiffWalk},{wynik.Sex},{wynik.Age},{wynik.Education},{wynik.Income}");




            }

        }
        public string ReadCsv(int idPacjenta)
        {
            string desiredFilePath = "...\\Projekt Towam\\ostateczny\\Wyniki";
            string csvFileName = $"wynik_pacjenta_{idPacjenta}_Diagnoza.csv";
            string csvFilePath = Path.Combine(desiredFilePath, csvFileName);

            bool fileExists = File.Exists(csvFilePath);
            if (fileExists)
            {
                using (StreamReader sr = new StreamReader(csvFilePath))
                {
                    //pierwszy wers pomijamy pomijamy
                    sr.ReadLine();
                    //drugi wiersz pierwsza kolumna to diagnoza
                    string diagnoza = sr.ReadLine();

                    return diagnoza;

                }
            }

            else
            {
                MessageBox.Show("Plik CSV nie został odnaleziony.Proszę wygeneruj plik z diagnozą pacjenta. ", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }

        }

    }
}
