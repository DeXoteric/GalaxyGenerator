namespace GalaxyGenerator
{
    public class Star
    {
        public string starName { get; protected set; }
        public int numberOfPlanets { get; protected set; }

        public Star(string starName, int numberOfPlanets)
        {
            this.starName = starName;
            this.numberOfPlanets = numberOfPlanets;
        }
    }
}