using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GalaxyGenerator
{
    public class Galaxy : MonoBehaviour
    {
        public static Galaxy instance;

        // TODO: Have these values import from user settings
        [SerializeField] private int numberOfStars = 300;
        [SerializeField] [Range(0, 50)] private int minimumRadius = 0;
        [SerializeField] [Range(70, 100)] private int maximumRadius = 100;
        [SerializeField] [Range(-1999999999, 1999999999)] private int seedNumber = 0;

        [SerializeField] private float minDistBetweenStars = 2f;

        public string[] planetTypes = { "Barren", "Terran", "Gas Giant" };

        public Dictionary<Star, GameObject> starToObjectMap { get; protected set; }

        private void OnEnable()
        {
            instance = this;
        }

        private void Start()
        {
            Random.InitState(seedNumber);
            starToObjectMap = new Dictionary<Star, GameObject>();

            int failCount = 0;

            for (int i = 0; i < numberOfStars; i++)
            {
                Star starData = new Star("Star " + i, Random.Range(2, 7));
                //Debug.Log("Created " + starData.starName + " with " + starData.numberOfPlanets + " planets");
                CreatePlanetData(starData);

                Vector3 position = PositionMath.RandomPosition(minimumRadius, maximumRadius);

                Collider[] positionCollider = Physics.OverlapSphere(position, minDistBetweenStars);

                if (positionCollider.Length == 0)
                {
                    GameObject star = SpaceObjects.CreateSphereObject(starData.starName, position, this.transform);
                    starToObjectMap.Add(starData, star);
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
                //Debug.Log(planetData.planetName + " " + planetData.planetType);

                star.planetList.Add(planetData);
            }
        }

        public Star ReturnStarFromGameObject(GameObject gameObject)
        {
            if (starToObjectMap.ContainsValue(gameObject))
            {
                int index = starToObjectMap.Values.ToList().IndexOf(gameObject);
                Star star = starToObjectMap.Keys.ToList()[index];

                return star;
            }
            else
            {
                return null;
            }
        }

        public void DestroyGalaxy()
        {
            while (transform.childCount > 0)
            {
                Transform go = transform.GetChild(0);
                go.SetParent(null);
                Destroy(go.gameObject);
            }
        }
    }
}