using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField]
    private ScoreManager scoreManager;

    public ScoreManager GetScoreManager() => scoreManager;
    public void ReloadScene()
    {
        // fast way to FULL reaload current scene (without any bug of missing smth)
        // for the demo will be OK
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 0 ? 1 : 0);
    }
}
