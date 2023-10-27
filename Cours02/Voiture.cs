
namespace Cours02
{
    /// <summary>
    /// Represents a specialized vehicle, specifically a car, inheriting properties and methods
    /// from the Vehicule base class.
    /// </summary>
    public class Voiture : Vehicule
    {
        #region Properties

        /// <summary>
        /// Gets the engine displacement in cubic centimeters.
        /// </summary>
        public double Cylindree { get; protected set; }

        /// <summary>
        /// Gets the number of doors on the car.
        /// </summary>
        public int NbPortes { get; protected set; }

        /// <summary>
        /// Gets the engine power in horsepower.
        /// </summary>
        public double Puissance { get; protected set; }

        /// <summary>
        /// Gets the current mileage of the car in kilometers.
        /// </summary>
        public double Kilometrage { get; protected set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the Voiture class with specified characteristics.
        /// </summary>
        /// <param name="marque">Brand of the car.</param>
        /// <param name="dateAchat">The date when the car was purchased.</param>
        /// <param name="prixDachat">The price at which the car was purchased.</param>
        /// <param name="cylindree">The engine displacement of the car in cubic centimeters.</param>
        /// <param name="nbPortes">The number of doors on the car.</param>
        /// <param name="puissance">The power of the car's engine in horsepower.</param>
        /// <param name="kilometrage">The car's mileage in kilometers.</param>
        public Voiture(string marque, DateTime dateAchat, decimal prixDachat, double cylindree, int nbPortes, double puissance, double kilometrage)
            : base(marque, dateAchat, prixDachat)
        {
            Cylindree = cylindree;
            NbPortes = nbPortes;
            Puissance = puissance;
            Kilometrage = kilometrage;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Calculates the current market price of the car, considering additional 
        /// factors such as mileage and brand influence on depreciation.
        /// </summary>
        /// <param name="utiliserArrondi">Indicates whether to round the depreciation
        /// based on mileage to the nearest 10,000 km increment or use the exact value.</param>
        protected override void CalculerPrixCourant(bool utiliserArrondi = false)
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
            
            PrixCourant = PrixAchat * (1 - depreciationTotale);
        }

        /// <summary>
        /// Returns a string representing the detailed information of this car, appending specific characteristics to the base vehicle data.
        /// </summary>
        /// <returns>A string containing the details of the car.</returns>
        public override string ToString()
        {
            return base.ToString() + "\n" +
                   $"Cylindrée: {Cylindree} cm3\n" +
                   $"Nombre de portes: {NbPortes}\n" +
                   $"Puissance: {Puissance} chevaux\n" +
                   $"Kilométrage: {Kilometrage:N1} km\n" +
                   "-----------------------------\n";

        }

        #endregion

    }
}