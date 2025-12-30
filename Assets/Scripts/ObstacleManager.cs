using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float despawnX = -10f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float obstacleSpeed = 5f;
    private float timer;
    private int index = 0;

    public bool soup = true;

    [SerializeField] private Boy boy;
    [SerializeField] private GameObject princess;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        Instance = this;
    }

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
        if (obstaclePrefabs.Length + 1 == index)
        {
            FindAnyObjectByType<RoadSpawner>().gameObject.SetActive(false);
            foreach (RoadPiece r in Resources.FindObjectsOfTypeAll<RoadPiece>())
            {
                r.speed = 0f;
            }
            StartCoroutine(Princess());
            index++;
        }
        if (obstaclePrefabs.Length < index) return;
        if (obstaclePrefabs.Length == index)
        {
            boy.Init();
            PanelManager.Instance.ShowPanel("Подберите мальчика", "Подъедьте к мальчику, чтобы подобрать его, и езжайте дальше", KeyCode.None, -1);
            FindAnyObjectByType<EnemyCarSpawner>().gameObject.SetActive(false);
            index++;
            return;
        }

        GameObject obs = obstaclePrefabs[index];
        GameObject obstacle = Instantiate(obs, new Vector2(spawnX, Random.Range(-2f, 2f)), Quaternion.identity);
        Obstacle obsScript = obstacle.GetComponent<Obstacle>();
        obsScript.Init(obstacleSpeed, despawnX);

        if (obsScript.isSoup) soup = false;

        PanelManager.Instance.ShowPanel(obsScript.label, obsScript.text, obsScript.button, obsScript.time);

        index++;
    }
    IEnumerator Princess()
    {
        float elapsed = 0;
        float duration = 3;

        Vector2 startPos = princess.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            princess.transform.position = Vector2.Lerp(startPos, new Vector2(7, 0), elapsed / duration);

            yield return null;
        }

        PanelManager.Instance.HidePanel();
        yield return new WaitForSeconds(2);

        winPanel.SetActive(true);
    }
}
