using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private float spawnDelay = 0.2f;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float y = 0f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float despawnX = -10f;
    [SerializeField] private int startCount = 20; // сколько кусков сразу

    private float timer;

    void Start()
    {
        // начальная дорога
        for (int i = 0; i < startCount; i++)
        {
            float x = spawnX - i * speed * spawnDelay;
            SpawnAt(x);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay && !GameManager.Instance.isGameOver)
        {
            timer = 0f;
            SpawnAt(spawnX);
        }
    }

    void SpawnAt(float x)
    {
        GameObject road = Instantiate(
            roadPrefab,
            new Vector3(x, y, 0),
            Quaternion.identity
        );

        road.AddComponent<RoadPiece>().Init(speed, despawnX);
    }
}
