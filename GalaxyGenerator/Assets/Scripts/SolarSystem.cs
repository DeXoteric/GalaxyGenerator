﻿using UnityEngine;
using UnityEngine.UI;

namespace GalaxyGenerator
{
    public class SolarSystem : MonoBehaviour
    {
        public static SolarSystem instance;

        [SerializeField] Button galaxyViewButton;

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
                Debug.Log("This star is called: " + star.starName + "\n" + "It has " + star.numberOfPlanets + " planets");

                Galaxy.instance.DestroyGalaxy();
                CreateSolarSystem(star);
            }
        }

        public void CreateSolarSystem(Star starData)
        {
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

            galaxyViewButton.interactable = false;
        }
    }
}