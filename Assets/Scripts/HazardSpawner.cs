using UnityEngine;

public static class SpawnerExtensions
{
    public static Vector3 GetPointInVolume(this Collider2D collider)
    {
        Vector3 result = Vector3.zero;
        result = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), collider.transform.position.y, 0F);

        return result;
    }
}

[RequireComponent(typeof(Collider2D))]
public class HazardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject hazardTemplate;
    [SerializeField]
    private GameObject debrisTemplate;
    [SerializeField]
    private GameObject invader;

    private GameObject instance;
    private Collider2D myCollider;

    [SerializeField]
    private float spawnFrequency = 1F;

    // Use this for initialization
    private void Start()
    {
        myCollider = GetComponent<Collider2D>();

        InvokeRepeating("SpawnEnemy", 0.2F, spawnFrequency);
    }

    private void SpawnEnemy()
    {
        if (hazardTemplate == null)
        {
            CancelInvoke();
        }
        else
        {
            int enemy = Random.Range(1, 4);
            Debug.Log("Enemy selected: " + enemy);

            if (enemy == 1)
            {
                instance = hazardTemplate;
                Instantiate(instance, myCollider.GetPointInVolume(), transform.rotation);
                Debug.Log("Standar Hazar created");
            }

            if (enemy == 2)
            {
                instance = debrisTemplate;
                Instantiate(instance, myCollider.GetPointInVolume(), transform.rotation);
                Debug.Log("Debris Hazar created");
            }

            if (enemy == 3)
            {
                instance = invader;
                Instantiate(instance, myCollider.GetPointInVolume(), transform.rotation);
                Debug.Log("Invader Hazar created");
            }

        }
    }
}