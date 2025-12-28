using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;

    [SerializeField] private RectTransform panel;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private TMP_Text textText;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Image timerImage;

    [SerializeField] private float startY = -133;
    [SerializeField] private float endY = 133;

    private float originalTimerWidth;

    private KeyCode currentButton = KeyCode.Space;

    private Coroutine timerCoroutine;

    private void Awake()
    {
        Instance = this;
        originalTimerWidth = timerImage.GetComponent<RectTransform>().sizeDelta.x;
    }

    private void Update()
    {
        if (Input.GetKeyDown(currentButton))
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
                StartCoroutine(Hide());
            }
        }
    }

    public void ShowPanel(string label, string text, KeyCode button, float time)
    {
        StartCoroutine(Show());
        timerCoroutine = StartCoroutine(Timer(time));
        currentButton = button;
        labelText.text = label;
        textText.text = text;
        buttonText.text = button.ToString();
        currentButton = button;
    }
    public void HidePanel()
    {
        StartCoroutine(Hide());
    }
    IEnumerator Show()
    {
        float elapsed = 0;
        float duration = 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            float ts = Mathf.SmoothStep(0, 1, t);

            panel.anchoredPosition = new Vector2(
                panel.anchoredPosition.x,
                Mathf.Lerp(startY, endY, ts)
            );

            yield return null;
        }

        panel.anchoredPosition = new Vector2(
            panel.anchoredPosition.x,
            endY
        );
    }
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(1);

        float elapsed = 0;
        float duration = 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            float ts = Mathf.SmoothStep(0, 1, t);

            panel.anchoredPosition = new Vector2(
                panel.anchoredPosition.x,
                Mathf.Lerp(endY, startY, ts)
            );

            yield return null;
        }

        panel.anchoredPosition = new Vector2(
            panel.anchoredPosition.x,
            startY
        );
    }
    IEnumerator Timer(float time)
    {
        float elapsed = 0;
        float duration = time;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            timerImage.GetComponent<RectTransform>().sizeDelta = new Vector2(
                Mathf.Lerp(originalTimerWidth, 0, elapsed / duration),
                timerImage.GetComponent<RectTransform>().sizeDelta.y
            );

            yield return null;
        }

        GameManager.Instance.isGameOver = true;
    }
}
