using UnityEngine;

namespace GalaxyGenerator
{
    public class SolarSystem : MonoBehaviour
    {
        public static SolarSystem instance;

        private void OnEnable()
        {
            instance = this;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
            {
                Star star = Galaxy.instance.ReturnStarFromGameObject(hit.transform.gameObject);
                Debug.Log("This star is called: " + star.starName + "\n" + "It has " + star.numberOfPlanets + " planets");

                Galaxy.instance.DestroyGalaxy();
                CreateSolarSystem(star);
            }
        }

       

        public void CreateSolarSystem(Star starData)
        {
            GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            star.name = starData.starName;
            star.transform.position = Vector3.zero;
            star.transform.SetParent(transform);

            for (int i = 0; i < starData.planetList.Count; i++)
            {
                Planet planet = starData.planetList[i];

                Vector3 planetPos = PositionMath.PlanetPosition(i);

                SpaceObjects.CreateSphereObject(planet.planetName, planetPos, this.transform);
            }
        }
    }
}