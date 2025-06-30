using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseEndGameController : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToGame()
    {
        SceneManager.UnloadSceneAsync("GamePause");
        Time.timeScale = 1f;
    }

    public void Play()
    {
        SceneManager.LoadScene("Level");
    }

    public void Back()
    {
        SceneManager.UnloadSceneAsync("Levels");
    }
}
