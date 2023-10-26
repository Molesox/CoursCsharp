using System.Globalization;

namespace Cours02
{



    public abstract class Vehicule
    {

        public string Marque { get; protected set; }
        public DateTime DateAchat { get; protected set; }
        public decimal PrixAchat { get; protected set; }
        public decimal PrixCourant { get; protected set; }

        public int Age
        {
            get
            {
                return DateTime.Today.Year - DateAchat.Year;
            }
        }


        public Vehicule(string marque, DateTime dateAchat, decimal prixDachat)
        {
            Marque = marque;
            DateAchat = dateAchat;
            PrixAchat = prixDachat;
            PrixCourant = prixDachat;
        }


        public virtual void CalculerPrix(bool utiliserArrondi = false)
        {
            const decimal AmortissementAnnuel = 0.01m;

            decimal amortissementTotal = PrixAchat * AmortissementAnnuel * Age;
            PrixCourant = PrixAchat - amortissementTotal;
        }


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
    }


}