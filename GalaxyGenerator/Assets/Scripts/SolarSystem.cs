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
            }
        }
    }
}