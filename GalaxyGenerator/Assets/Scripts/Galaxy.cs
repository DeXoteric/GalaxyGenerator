using UnityEngine;

public class Galaxy : MonoBehaviour
{
    [SerializeField] private int numberOfStars = 300;
    [SerializeField] private int maximumRadius = 100;

    private void Start()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
           GameObject start=  GameObject.CreatePrimitive(PrimitiveType.Sphere);

            float distance = Random.Range(0, maximumRadius);
            float angle = Random.Range(0, 2 * Mathf.PI);

            Vector3 position = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);

            start.transform.position = position;
        }
    }
}