using UnityEngine;

public class EnemyCarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private Vector2 yRange = new Vector2(-2f, 2f);
    [SerializeField] private float speed = 4f;
    [SerializeField] private float despawnX = -10f;
    [SerializeField] private Vector2 delayRange = new Vector2(1, 2);

    private float timer;
    private float spawnDelay = 2f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay && !GameManager.Instance.isGameOver)
        {
            timer = 0f;
            Spawn();
            spawnDelay = Random.Range(delayRange.x, delayRange.y);
        }
    }

    void Spawn()
    {
        if (carPrefabs.Length == 0) return;

        GameObject prefab =
            carPrefabs[Random.Range(0, carPrefabs.Length)];

        float y = Random.Range(yRange.x, yRange.y);

        GameObject car = Instantiate(
            prefab,
            new Vector3(spawnX, y, 0),
            Quaternion.identity
        );

        car.AddComponent<EnemyCar>().Init(speed, despawnX);
    }
}
