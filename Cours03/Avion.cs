
namespace Cours03
{
    public enum AvionType
    {
        Helice,
        Reaction
    }
    public class Avion : Vehicule
    {
        public AvionType Type { get; protected set; }
        public decimal HrsDeVol { get; protected set; }


        public Avion(string marque, DateTime dateAchat, decimal prixDachat, AvionType type, decimal hrsDeVol)
         : base(marque, dateAchat, prixDachat)
        {
            Type = type;
            HrsDeVol = hrsDeVol;
        }

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

        protected override void EffectuerInspectionSpecifique()
        {
            if (HrsDeVol < 10_000)
                Console.WriteLine("L'avion n'a pas assez d'heures de vol");
            else
                Console.WriteLine("L'avion a trop d'heures de vol");

        }

        public override string ToString()
        {
            return base.ToString() + "\n" +
                   $"Type d'avion: {Type}\n" +
                   $"Heures de vol: {HrsDeVol:N1}\n" +
                   "-----------------------------\n";

        }
    }
}