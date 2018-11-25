using UnityEngine;

namespace GalaxyGenerator
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float panSpeed = 100f;

        private float minCameraOrthoSize = 30;
        private float defaultCameraOrthoSize = 50;
        private float maxCameraOrthoSize = 100;
        private float zoomLevel = 0;
        private bool inverseZoom = false;

        private void Start()
        {
            ResetCamera();
        }

        private void Update()
        {
            ChangePosition();
            ChangeZoom();
        }

        public void ResetCamera()
        {
            transform.position = new Vector3(0, 0, -100);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            zoomLevel = 0;
            Camera.main.orthographicSize = defaultCameraOrthoSize;
        }

        private void ChangePosition()
        {
            if (Galaxy.instance.galaxyView == true)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    float distance = panSpeed * Time.deltaTime;
                    Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;

                    float dampingFactor = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

                    transform.Translate(distance * direction * dampingFactor);

                    ClampCameraPan();
                }
            }
        }

        private void ClampCameraPan()
        {
            Vector3 position = transform.position;

            if (Galaxy.instance.galaxyView == true)
            {
                position.x = Mathf.Clamp(transform.position.x, -Galaxy.instance.maximumRadius, Galaxy.instance.maximumRadius);
                position.y = Mathf.Clamp(transform.position.y, -Galaxy.instance.maximumRadius, Galaxy.instance.maximumRadius);
            }

            transform.position = position;
        }

        private void ChangeZoom()
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                if (inverseZoom)
                {
                    zoomLevel = Mathf.Clamp01(zoomLevel - Input.GetAxis("Mouse ScrollWheel"));
                }
                else
                {
                    zoomLevel = Mathf.Clamp01(zoomLevel + Input.GetAxis("Mouse ScrollWheel"));
                }
            }

            float zoom = Mathf.Lerp(maxCameraOrthoSize, minCameraOrthoSize, zoomLevel);
            Camera.main.orthographicSize = zoom;
        }
    }
}