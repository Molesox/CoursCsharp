namespace Cours02
{
    public class Cercle : Forme
    {
        private double rayon;

        public Cercle(double rayon)
        {
            this.rayon = rayon;
        }

        public override double Aire()
        {
            return (double)(Math.PI * Math.Pow(rayon, 2));
        }
    }
}
