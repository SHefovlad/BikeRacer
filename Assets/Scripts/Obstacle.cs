using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public string label;
    public string text;
    public KeyCode button;
    public float time;

    private float speed;
    private float despawnX;

    public bool isSoup = false;
    public void Init(float speed, float despawnX)
    {
        this.speed = speed;
        this.despawnX = despawnX;
    }
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x <= despawnX)
        {
            Destroy(gameObject);
            if (!ObstacleManager.Instance.soup) GameManager.Instance.isGameOver = true;
        }
    }
}
