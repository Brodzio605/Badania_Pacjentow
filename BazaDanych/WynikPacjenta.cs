using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTOWAM.BazaDanych
{
    public class WynikPacjenta
    {
        [Required]
        public int Id { get; set; }
        public int IdPacjenta { get; set; }
        public int Diabetes012 { get; set; }
        public int HighBP { get; set; }
        public int HighChol { get; set; }
        public int CholCheck { get; set; }
        public decimal BMI { get; set; }
        public int Smoker { get; set; }
        public int Stroke { get; set; }
        public int HeartDiseaseorAttack { get; set; }
        public int PhysActivity { get; set; }
        public int Fruits { get; set; }
        public int Veggies { get; set; }
        public int HvyAlcoholConsump { get; set; }
        public int AnyHealthcare { get; set; }
        public int NoDocbcCost { get; set; }
        public int GenHlth { get; set; }
        public int MentHlth { get; set; }
        public int PhysHlth { get; set; }
        public int DiffWalk { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Education { get; set; }
        public int Income { get; set; }

        // metoda w której przekazujemy wyniki pacjenta, taka sama jak w DaneOsobowePacjentów
        public void Aktualizuj(WynikPacjenta wyniki)
        {
            if (wyniki == null)
                return;

            Diabetes012 = wyniki.Diabetes012;
            HighBP = wyniki.HighBP;
            HighChol = wyniki.HighChol;
            CholCheck = wyniki.CholCheck;
            BMI = wyniki.BMI;
            Smoker = wyniki.Smoker;
            Stroke = wyniki.Stroke;
            HeartDiseaseorAttack = wyniki.HeartDiseaseorAttack;
            PhysActivity = wyniki.PhysActivity;
            Fruits = wyniki.Fruits;
            Veggies = wyniki.Veggies;
            HvyAlcoholConsump = wyniki.HvyAlcoholConsump;
            AnyHealthcare = wyniki.AnyHealthcare;
            NoDocbcCost = wyniki.NoDocbcCost;
            GenHlth = wyniki.GenHlth;
            MentHlth = wyniki.MentHlth;
            PhysHlth = wyniki.PhysHlth;
            DiffWalk = wyniki.DiffWalk;
            Sex = wyniki.Sex;
            Age = wyniki.Age;
            Education = wyniki.Education;
            Income = wyniki.Income;
        }
    }
}
