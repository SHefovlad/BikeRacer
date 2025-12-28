using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float despawnX = -10f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float obstacleSpeed = 5f;
    private float timer;
    private int index = 0;

    void Update()
    {
        if (GameManager.Instance.isGameOver) return;
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }
    void SpawnObstacle()
    {
        if (obstaclePrefabs.Length <= index) return;

        GameObject obs = obstaclePrefabs[index];
        GameObject obstacle = Instantiate(obs, new Vector2(spawnX, Random.Range(-2f, 2f)), Quaternion.identity);
        Obstacle obsScript = obstacle.GetComponent<Obstacle>();
        obsScript.Init(obstacleSpeed, despawnX);

        PanelManager.Instance.ShowPanel(obsScript.label, obsScript.text, obsScript.button, obsScript.time);

        index++;
    }
}
