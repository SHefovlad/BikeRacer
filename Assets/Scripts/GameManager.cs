using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    bool _isgameOver = false;
    public bool isGameOver
    {
        get { return _isgameOver; } 
        set
        {
            _isgameOver = value;
            gameOverPanel.SetActive(value);
        }
    }

    public GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
