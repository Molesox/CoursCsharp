using System.Globalization;

namespace Cours02
{

    /// <summary>
    /// Represents a base vehicle with common properties and behaviors.
    /// This class is abstract and intended to be inherited by specific vehicle types.
    /// </summary>
    public abstract class Vehicule
    {
        #region Properties

        /// <summary>
        /// Gets the brand of the vehicle.
        /// </summary>
        public string Marque { get; protected set; }

        /// <summary>
        /// Gets the purchase date of the vehicle.
        /// </summary>
        public DateTime DateAchat { get; protected set; }

        /// <summary>
        /// Gets the original purchase price of the vehicle.
        /// </summary>
        public decimal PrixAchat { get; protected set; }

        /// <summary>
        /// Gets the current market price of the vehicle.
        /// </summary>
        public decimal PrixCourant
        {
            get
            {

                CalculerPrix();
                return _prixCourant;
            }
            protected set
            {
                _prixCourant = value < 0 ? 0 : value;
            }
        }
        private decimal _prixCourant;

        /// <summary>
        /// Calculates the age of the vehicle based on the current year.
        /// </summary>
        public int Age
        {
            get
            {
                return DateTime.Today.Year - DateAchat.Year;
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the Vehicule class with specified brand, purchase date, and purchase price.
        /// </summary>
        /// <param name="marque">Brand of the vehicle.</param>
        /// <param name="dateAchat">The date when the vehicle was purchased.</param>
        /// <param name="prixDachat">The price at which the vehicle was purchased.</param>
        public Vehicule(string marque, DateTime dateAchat, decimal prixDachat)
        {
            Marque = marque;
            DateAchat = dateAchat;
            PrixAchat = prixDachat;

        }

        #endregion

        /// <summary>
        /// Calculates the current price of the vehicle based on its purchase price and other criteria.
        /// The actual calculation will be implemented in derived classes, considering the specifics of each vehicle type.
        /// </summary>
        /// <param name="utiliserArrondi">Indicates whether rounding should be applied during calculation.</param>
        public abstract void CalculerPrix(bool utiliserArrondi = false);

        #region Overrides

        /// <summary>
        /// Returns a string that represents the current vehicle, displaying detailed information including brand, purchase date, age, and price details.
        /// </summary>
        /// <returns>A string that represents the current vehicle object.</returns>
        public override string ToString()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;

            return "-----------------------------\n" +
                   $"Marque: {Marque}\n" +
                   $"Date d'achat: {DateAchat.ToString("d", culture)}\n" +
                   $"Âge du véhicule: {Age} années\n" +
                   $"Prix d'achat: {PrixAchat.ToString("C2", culture)}\n" +
                   $"Prix courant: {PrixCourant.ToString("C2", culture)}";
        }

        #endregion

    }


}