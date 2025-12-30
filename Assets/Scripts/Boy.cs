using System.Collections;
using UnityEngine;

public class Boy : MonoBehaviour
{
    public void Init()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        float elapsed = 0;
        float duration = 5;

        Vector3 startPos = transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(startPos, Vector3.zero, elapsed / duration);

            yield return null;
        }

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.SetParent(collision.transform);
            transform.localPosition = new Vector2(-0.15f, -0.15f);
        }
    }
}
