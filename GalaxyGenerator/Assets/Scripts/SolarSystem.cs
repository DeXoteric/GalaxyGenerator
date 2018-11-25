using UnityEngine;
using UnityEngine.UI;

namespace GalaxyGenerator
{
    public class SolarSystem : MonoBehaviour
    {
        public static SolarSystem instance;

        [SerializeField] private Button galaxyViewButton;

        public Vector3 starPosition { get; set; }

        private void OnEnable()
        {
            instance = this;
            galaxyViewButton.interactable = false;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
            {
                Star star = Galaxy.instance.ReturnStarFromGameObject(hit.transform.gameObject);
                starPosition = hit.transform.position;
                Debug.Log("This star is called: " + star.starName + "\n" + "It has " + star.numberOfPlanets + " planets");

                Galaxy.instance.galaxyView = false;
                Galaxy.instance.DestroyGalaxy();
                CreateSolarSystem(star);
            }
        }

        public void CreateSolarSystem(Star starData)
        {
            CameraController.instance.SolarSystemCameraSettings();

            SpaceObjects.CreateSphereObject(starData.starName, Vector3.zero, transform);

            for (int i = 0; i < starData.planetList.Count; i++)
            {
                Planet planet = starData.planetList[i];

                Vector3 planetPos = PositionMath.PlanetPosition(i);

                SpaceObjects.CreateSphereObject(planet.planetName, planetPos, this.transform);
            }

            galaxyViewButton.interactable = true;
        }

        public void DestroySolarSystem()
        {
            while (transform.childCount > 0)
            {
                Transform go = transform.GetChild(0);
                go.SetParent(null);
                Destroy(go.gameObject);
            }

            CameraController.instance.GalaxyViewCameraSettings();
            CameraController.instance.MoveTo(starPosition);

            galaxyViewButton.interactable = false;
        }
    }
}