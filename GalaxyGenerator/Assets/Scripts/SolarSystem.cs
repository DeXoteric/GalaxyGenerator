﻿using UnityEngine;
using UnityEngine.UI;

namespace GalaxyGenerator
{
    public class SolarSystem : MonoBehaviour
    {
        public static SolarSystem instance;

        [SerializeField] private Button galaxyViewButton;
        [SerializeField] private GameObject orbitSpritePrefab;

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

            if (Physics.Raycast(ray, out hit))
            {
                Galaxy.instance.MoveSelectionIcon(hit);

                if (Input.GetMouseButtonDown(0) && Galaxy.instance.galaxyView == true)
                {
                    Star star = Galaxy.instance.ReturnStarFromGameObject(hit.transform.gameObject);
                    starPosition = hit.transform.position;
                    Debug.Log("This star is called: " + star.starName + "\n" + "It has " + star.numberOfPlanets + " planets");

                    Galaxy.instance.galaxyView = false;
                    Galaxy.instance.DestroyGalaxy();
                    CreateSolarSystem(star);
                }
            } else
            {
                Galaxy.instance.selectionIcon.SetActive(false);
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

                GameObject orbit = SpaceObjects.CreateOrbitPath(orbitSpritePrefab, planet.planetName + " Orbit", i + 1, transform);
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