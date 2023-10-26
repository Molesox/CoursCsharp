namespace Cours03
{

    public class Balle : ISupportNewton
    {
        public double Masse { get; set; } = 2;
        public double Acceleration { get; set; } = 4;
        public string Couleur { get; set; } = "blanc";

        public override string ToString()
        {
 
            return $"La balle est de couleur {Couleur}";
        }

    }
}