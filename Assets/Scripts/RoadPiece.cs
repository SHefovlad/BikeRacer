using UnityEngine;

public class RoadPiece : MonoBehaviour
{
    public float speed;
    private float despawnX;

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
            Destroy(gameObject);
    }
}
