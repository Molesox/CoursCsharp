using System.Globalization;

namespace Cours03
{
    public abstract class Vehicule
    {

        public string Marque { get; protected set; }
        public DateTime DateAchat { get; protected set; }
        public decimal PrixAchat { get; protected set; }
        private decimal _prixCourant;
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


        public abstract void CalculerPrix(bool utiliserArrondi = false);

        public void PreparerPourVente()
        {
            CalculerPrix();
            EffectuerInspectionStandard();
            EffectuerInspectionSpecifique();
        }


        protected virtual void EffectuerInspectionStandard()
        {
            if (Age > 30)
                Console.WriteLine("Le véhicule est trop vieux");
            else
                Console.WriteLine("Le véhicule est correct");

        }

        protected abstract void EffectuerInspectionSpecifique();

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