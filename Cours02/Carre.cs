namespace Cours02
{
    public class Carre : Forme
    {
        private double cote;

        public Carre(double cote)
        {
            this.cote = cote;
        }

        public override double Aire()
        {
            return (double)Math.Pow(cote, 2);
        }
    }
}