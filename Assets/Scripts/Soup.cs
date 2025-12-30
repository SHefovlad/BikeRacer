using UnityEngine;

public class Soup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ObstacleManager.Instance.soup = true;
        }
    }
}
