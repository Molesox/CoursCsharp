
using System.Runtime.InteropServices;

namespace Cours03
{
    public class Voiture : Vehicule, ISupportNewton
    {

        public double Cylindree { get; protected set; }
        public int NbPortes { get; protected set; }
        public double Puissance { get; protected set; }
        public double Kilometrage { get; protected set; }

        public double Masse { get ; set ; }
        public double Acceleration { get ; set ; }

        public Voiture(string marque, DateTime dateAchat, decimal prixDachat, double cylindree, int nbPortes, double puissance, double kilometrage)
        : base(marque, dateAchat, prixDachat)
        {
            Cylindree = cylindree;
            NbPortes = nbPortes;
            Puissance = puissance;
            Kilometrage = kilometrage;
        }

        public Voiture(string marque, DateTime dateAchat, decimal prixDachat, double masse, double acceleration)
         : base(marque, dateAchat, prixDachat)
        {
            Masse = masse;
            Acceleration = acceleration;
        }

        public override string ToString()
        {
            return base.ToString() + "\n" +
                   $"Cylindrée: {Cylindree} cm3\n" +
                   $"Nombre de portes: {NbPortes}\n" +
                   $"Puissance: {Puissance} chevaux\n" +
                   $"Kilométrage: {Kilometrage:N1} km\n" +
                   "-----------------------------\n";

        }


        protected override void EffectuerInspectionSpecifique()
        {
            if (Kilometrage > 100_000)
                Console.WriteLine("Le kilometrage est trop haut");
            else
                Console.WriteLine("Le kilometrage est bon");

        }


        public override void CalculerPrix(bool utiliserArrondi = false)
        {

            decimal depreciationParAn = 0.02m * Age; // 2% par an.

            decimal depreciationParKilometrage;

            if (utiliserArrondi)
            {

                int tranchesKilometrage = (int)Math.Round(Kilometrage / 10_000); // arrondi à la tranche la plus proche.
                depreciationParKilometrage = 0.05m * tranchesKilometrage; // 5% par tranche de 10000 km.
            }
            else
            {
                depreciationParKilometrage = 0.05m * (decimal)Kilometrage / 10000.0m; // 5% par 10000 km exacts.
            }

            // Ajuster la dépréciation basée sur la marque.
            decimal ajustementMarque = 0m; // Pas de changement pour les autres marques.
            if (Marque == "Renault" || Marque == "Fiat")
            {
                ajustementMarque = -0.10m; // Réduire de 10%.
            }
            else if (Marque == "Ferrari" || Marque == "Porsche")
            {
                ajustementMarque = 0.20m; // Augmenter de 20%.
            }

            decimal depreciationTotale = depreciationParAn + depreciationParKilometrage + ajustementMarque;

            PrixCourant = PrixAchat * (1 - depreciationTotale);
        }

        public double GetForce()
        {
           return Masse * Acceleration;
        }
    }
}