using System.Collections.Generic;

namespace GalaxyGenerator
{
    public class Star
    {
        public string starName { get; protected set; }
        public int numberOfPlanets { get; protected set; }

        public List<Planet> planetList;

        public Star(string starName, int numberOfPlanets)
        {
            this.starName = starName;
            this.numberOfPlanets = numberOfPlanets;

            planetList = new List<Planet>();
        }
    }
}