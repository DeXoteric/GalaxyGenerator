using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 100f;

    private void Start()
    {
        GalaxyViewCamera();
    }

    private void Update()
    {
        ChangePosition();
    }

    public void GalaxyViewCamera()
    {
        transform.position = new Vector3(0, 0, -100);

        Camera.main.orthographicSize = 50;
    }

    public void SolarSystemViewCamera()
    {
        transform.position = new Vector3(0, 60, -100);

        Camera.main.orthographicSize = 50;
    }

    private void ChangePosition()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float distance = panSpeed * Time.deltaTime;
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;

            float dampingFactor = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));
            
            transform.Translate(distance * direction * dampingFactor);
        }
    }
}