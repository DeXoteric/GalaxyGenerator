using UnityEngine;

namespace GalaxyGenerator
{
    public class Galaxy : MonoBehaviour
    {

        // TODO: Have these values import from user settings
        [SerializeField] private int numberOfStars = 300;
        [SerializeField] [Range(0, 50)] private int minimumRadius = 0;
        [SerializeField] [Range(70, 100)] private int maximumRadius = 100;

        [SerializeField] private float minDistBetweenStars = 2f;

        public string[] planetTypes = { "Barren", "Terran", "Gas Giant" };

        private void Start()
        {
            int failCount = 0;

            for (int i = 0; i < numberOfStars; i++)
            {
                Star starData = new Star("Star " + i, Random.Range(2, 7));
                //Debug.Log("Created " + starData.starName + " with " + starData.numberOfPlanets + " planets");
                CreatePlanetData(starData);

                Vector3 position = RandomPosition();

                Collider[] positionCollider = Physics.OverlapSphere(position, minDistBetweenStars);

                if (positionCollider.Length == 0)
                {
                    GameObject star = CreateStar(starData, position);

                    failCount = 0;
                }
                else
                {
                    i--;
                    failCount++;
                }

                if (failCount > numberOfStars)
                {
                    Debug.LogError("Could not fit all stars in the galaxy. Consider smaller distance between stars!");
                    break;
                }
            }
        }

        // This method creates a sphere object using the built in sphere model in unity
        private GameObject CreateStar(Star starData, Vector3 position)
        {
            GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            star.name = starData.starName;
            star.transform.position = position;

            return star;
        }

        //This method creates a random polar coordinate then converts and returns it as a Cartesian coordinate
        private Vector3 RandomPosition()
        {
            float distance = Random.Range(minimumRadius, maximumRadius);
            float angle = Random.Range(0, 2 * Mathf.PI);

            Vector3 position = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);

            return position;
        }

        // This method creates all the planet data for a star
        private void CreatePlanetData(Star star)
        {
            for (int i = 0; i < star.numberOfPlanets; i++)
            {
                string name = star.starName + (star.planetList.Count + 1).ToString();

                int random = Random.Range(1, 100);
                string type = "";

                if (random < 40)
                {
                    type = planetTypes[0];
                }
                else if (40 <= random && random < 50)
                {
                    type = planetTypes[1];
                }
                else
                {
                    type = planetTypes[2];
                }

                Planet planetData = new Planet(name, type);
                Debug.Log(planetData.planetName + " " + planetData.planetType);

                star.planetList.Add(planetData);
            }
        }
    }
}