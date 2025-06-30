using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Header("For Level Init")]
    [SerializeField] LevelCounter _levelCounter;
    [SerializeField] LevelConfig _level1;
    [SerializeField] LevelConfig _level2;
    [SerializeField] LevelConfig _level3;

    [Header("Managers")]
    [Inject] UIManager _uiManager;
    [Inject] Room _room;

    bool isPaused;

    public delegate void EventHandler();
    public static EventHandler Handler;

    void Awake()
    {
        Application.targetFrameRate = 120;
        SceneManager.sceneLoaded += OnLevelChange;
        Handler = OnPlayerDead;

    }

    void Start()
    {
        switch (_levelCounter.ActiveLevel)
        {
            case 1:
                InitializeLevel(_level1);
                break;
            case 2:
                InitializeLevel(_level2);
                break;
            case 3:
                InitializeLevel(_level3);
                break;
        }
    }

    void InitializeLevel(LevelConfig level)
    {
        _uiManager._nextLevelButttonText.text = level.NextButtonLevelText;
        _room.StartText = level.StartText;
        _room.BossText = level.BossText;
    }

    void OnPlayerDead()
    {
        SceneManager.LoadScene("PlayerDead", LoadSceneMode.Additive);
        _levelCounter.ActiveLevel = 1;
    }

    public void NextLevel()
    {
        if (_levelCounter.ActiveLevel == 1)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            _levelCounter.ActiveLevel = 1;
            return;
        }
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        _levelCounter.ActiveLevel++;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("GamePause", LoadSceneMode.Additive);
    }

    void OnLevelChange(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= OnLevelChange;
    }
}
