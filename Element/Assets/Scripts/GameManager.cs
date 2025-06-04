using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void EventHandler();
    public static EventHandler Handler;

    void Awake()
    {
        Application.targetFrameRate = 120;
    }
    void Start()
    {
        Handler = OnPlayerDead;
    }

    void OnPlayerDead()
    {
        Debug.Log("PlayerDead");
    }
}
