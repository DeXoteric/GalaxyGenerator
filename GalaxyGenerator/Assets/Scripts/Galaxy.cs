using UnityEngine;

namespace GalaxyGenerator
{
    public class Galaxy : MonoBehaviour
    {
        [SerializeField] private int numberOfStars = 300;
        [SerializeField] [Range(0, 50)] private int minimumRadius = 0;
        [SerializeField] [Range(70, 100)] private int maximumRadius = 100;
        [SerializeField] private float minDistBetweenStars = 2f;

        private void Start()
        {
            int failCount = 0;

            for (int i = 0; i < numberOfStars; i++)
            {
                Star starData = new Star("Star " + i, Random.Range(2, 7));
                Debug.Log("Created " + starData.starName + " with " + starData.numberOfPlanets + " planets");

                float distance = Random.Range(minimumRadius, maximumRadius);
                float angle = Random.Range(0, 2 * Mathf.PI);

                Vector3 position = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);

                Collider[] positionCollider = Physics.OverlapSphere(position, minDistBetweenStars);

                if (positionCollider.Length == 0)
                {
                    GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    star.name = starData.starName;
                    star.transform.position = position;

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
    }
}