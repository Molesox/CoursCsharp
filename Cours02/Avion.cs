
namespace Cours02
{
    /// <summary>
    /// Enumeration of the types of planes, differentiating between propeller-driven and jet planes.
    /// </summary>
    public enum AvionType
    {
        Helice,
        Reaction
    }

    /// <summary>
    /// Represents a specific type of vehicle, namely an airplane, inheriting properties 
    /// and methods from the Vehicule base class.
    /// </summary>
    public class Avion : Vehicule
    {
        #region Properties

        /// <summary>
        /// Gets the type of airplane, categorized by its engine system.
        /// </summary>
        public AvionType Type { get; protected set; }

        /// <summary>
        /// Gets the total flight hours of the airplane.
        /// </summary>
        public decimal HrsDeVol { get; protected set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the Avion class with specified characteristics.
        /// </summary>
        /// <param name="marque">Brand of the airplane.</param>
        /// <param name="dateAchat">The date when the airplane was purchased.</param>
        /// <param name="prixDachat">The price at which the airplane was purchased.</param>
        /// <param name="type">The type of airplane, defined by the AvionType enumeration.</param>
        /// <param name="hrsDeVol">Total hours the airplane has flown.</param>
        public Avion(string marque, DateTime dateAchat, decimal prixDachat, AvionType type, decimal hrsDeVol)
         : base(marque, dateAchat, prixDachat)
        {
            Type = type;
            HrsDeVol = hrsDeVol;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Calculates the current market price of the airplane,
        /// considering factors such as type of airplane and total flight hours.
        /// </summary>
        /// <param name="utiliserArrondi">Indicates whether to round the depreciation
        /// based on flight hours to the nearest interval increment or use the exact value.</param>
        public override void CalculerPrix(bool utiliserArrondi = false)
        {
            decimal facteurTranche = Type switch
            {
                AvionType.Reaction => 1000m, // La tranche pour les avions à réaction est de 1000 heures.
                AvionType.Helice => 100m, // La tranche pour les avions à hélice est de 100 heures.
                _ => throw new InvalidOperationException("Type d'avion non reconnu.")
            };

            decimal tranchesHrsVol = utiliserArrondi
                ? Math.Round(HrsDeVol / facteurTranche) // Arrondi au nombre entier de tranches.
                : HrsDeVol / facteurTranche; // Utilise le nombre exact de tranches.

            // Calculer le prix courant en appliquant la dépréciation.
            PrixCourant = PrixAchat * (1.0m - 0.1m * tranchesHrsVol); // Dépréciation de 10% par tranche d'heures de vol.
        }

        /// <summary>
        /// Returns a string representing the detailed information of this airplane, 
        /// appending specific characteristics to the base vehicle data.
        /// </summary>
        /// <returns>A string containing the details of the airplane.</returns>
        public override string ToString()
        {
            return base.ToString() + "\n" +
                   $"Type d'avion: {Type}\n" +
                   $"Heures de vol: {HrsDeVol:N1}\n" +
                   "-----------------------------\n";

        }

        #endregion
    }
}