namespace Cours02
{
    public class Person
    {
        private int age;
        private int maxHeartRate; // la fréquence cardiaque maximale

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                    throw new Exception("L'âge ne peut pas être négatif!");
                age = value;
            }
        }

        public int MaxHeartRate
        {
            get { return maxHeartRate; }
            set
            {
                if (value < 0)
                    throw new Exception("La fréquence cardiaque maximale ne peut pas être négative!"); //(validité)
                if (value > (220 - age))
                    throw new Exception("La fréquence cardiaque maximale est trop élevée pour cet âge!"); // Vérification de l'intégrité
                maxHeartRate = value;
            }
        }
    }

    public class Person2
    {
        public int Age { get; private set; }
        public int MaxHeartRate { get; private set; }

        public void SetAge(int value)
        {
            if (value < 0)
                throw new Exception("L'âge ne peut pas être négatif!");

            Age = value;

            // Ensure the max heart rate remains valid after age update
            if (MaxHeartRate > 220 - value)
            {
                SetMaxHeartRate(220 - value);
            }
        }

        public void SetMaxHeartRate(int value)
        {
            if (value < 0)
                throw new Exception("La fréquence cardiaque maximale ne peut pas être négative!");

            if (value > 220 - Age)
                throw new Exception("La fréquence cardiaque maximale est trop élevée pour cet âge!");

            MaxHeartRate = value;
        }
    }
}