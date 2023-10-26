
namespace Cours02
{
    public class Voiture : Vehicule
    {

        public double Cylindree {get; protected set;}
        public int NbPortes {get; protected set;}
        public double Puissance {get; protected set;}
        public double Kilometrage {get; protected set;}

        public Voiture(string marque, DateTime dateAchat, decimal prixDachat, double cylindree, int nbPortes, double puissance, double kilometrage)
        : base(marque, dateAchat, prixDachat)
        {
            Cylindree = cylindree;
            NbPortes = nbPortes;
            Puissance = puissance;
            Kilometrage = kilometrage;
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

        public override void CalculerPrix(bool utiliserArrondi=false)
        {

            // Calculer la dépréciation basée sur l'âge de la voiture.
            decimal depreciationParAn = 0.02m * Age; // 2% par an.

            // Déclarer une variable pour la dépréciation basée sur le kilométrage.
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
                ajustementMarque = -0.10m; // Réduire de 10% pour certaines marques.
            }
            else if (Marque == "Ferrari" || Marque == "Porsche")
            {
                ajustementMarque = 0.20m; // Augmenter de 20% pour les marques premium.
            }

            // Calculer le prix courant en appliquant tous les ajustements.
            decimal depreciationTotale = depreciationParAn + depreciationParKilometrage + ajustementMarque;
            PrixCourant = Math.Max(0.00m, PrixAchat * (1 - depreciationTotale));
        }

    }
}