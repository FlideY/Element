using TMPro;
using UnityEngine;
using Zenject;

public class Room : MonoBehaviour
{
    [Header("DoorsColliders")]
    [SerializeField] Collider2D _topDoor;
    [SerializeField] Collider2D _rightDoor;
    [SerializeField] Collider2D _leftDoor;
    [SerializeField] Collider2D _bottomDoor;

    [Header("Arrows")]
    [SerializeField] GameObject _topArrow;
    [SerializeField] GameObject _rightArrow;
    [SerializeField] GameObject _leftArrow;
    [SerializeField] GameObject _bottomArrow;

    [Header("RoomSprites")]
    [SerializeField] Sprite _startRoomSprite;
    [SerializeField] Sprite _normalRoomSprite;
    [SerializeField] Sprite _bossRoomSprite;

    [Header("Text")]
    [SerializeField] GameObject Text;

    SpriteRenderer _spriteRenderer;
    [Inject] EnvironmentManager _environmentManager;
    [HideInInspector] public RoomData RoomData;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Build()
    {
        tag = RoomData.Tag;
        GetRoomSprite(tag);
        SwitchText(tag);
        ReUnlockDoors();
        _environmentManager.SetEnvironment(RoomData.EnvironmentIndex);

        EventSendler(tag);
    }

    void GetRoomSprite(string tag)
    {
        switch (tag)
        {
            case "Start":
                _spriteRenderer.sprite = _startRoomSprite;
                break;
            case "Normal":
                _spriteRenderer.sprite = _normalRoomSprite;
                break;
            case "Boss":
                _spriteRenderer.sprite = _bossRoomSprite;
                break;
        }
    }

    void SwitchText(string tag)
    {
        switch (tag)
        {
            case "Start":
                Text.SetActive(true);
                Text.GetComponent<TextMeshPro>().text = "Модуль №1";
                break;
            case "Normal":
                Text.SetActive(false);
                break;
            case "Boss":
                Text.SetActive(true);
                Text.GetComponent<TextMeshPro>().text = "РK №1";
                break;
        }
    }
    public void ReUnlockDoors()
    {
        try
        {
            if (LevelGenerator.RoomGrid[RoomData.RoomIndex.x, RoomData.RoomIndex.y + 1] == 1)
            { _topDoor.isTrigger = true; _topArrow.SetActive(true); }
            else
            { _topDoor.isTrigger = false; _topArrow.SetActive(false); }
        }
        catch (System.IndexOutOfRangeException) { _topDoor.isTrigger = false; _topArrow.SetActive(false); }

        try
        {
            if (LevelGenerator.RoomGrid[RoomData.RoomIndex.x + 1, RoomData.RoomIndex.y] == 1)
            { _rightDoor.isTrigger = true; _rightArrow.SetActive(true); }
            else
            { _rightDoor.isTrigger = false; _rightArrow.SetActive(false); }
        }
        catch (System.IndexOutOfRangeException) { _rightDoor.isTrigger = false; _rightArrow.SetActive(false); }

        try
        {
            if (LevelGenerator.RoomGrid[RoomData.RoomIndex.x - 1, RoomData.RoomIndex.y] == 1)
            { _leftDoor.isTrigger = true; _leftArrow.SetActive(true); }
            else
            { _leftDoor.isTrigger = false; _leftArrow.SetActive(false); }
        }
        catch (System.IndexOutOfRangeException) { _leftDoor.isTrigger = false; _leftArrow.SetActive(false); }

        try
        {
            if (LevelGenerator.RoomGrid[RoomData.RoomIndex.x, RoomData.RoomIndex.y - 1] == 1)
            { _bottomDoor.isTrigger = true; _bottomArrow.SetActive(true); }
            else
            { _bottomDoor.isTrigger = false; _bottomArrow.SetActive(false); }
        }
        catch (System.IndexOutOfRangeException) { _bottomDoor.isTrigger = false; _bottomArrow.SetActive(false); }
    }

    void EventSendler(string tag)
    {
        switch (tag)
        {
            case "Normal":
                EnemyManager.Handler();
                break;
            case "Boss":
                BossManager.Handler();
                break;
        }
    }

    public void CloseDoors()
    {
        _topDoor.isTrigger = false;
        _topArrow.SetActive(false);

        _leftDoor.isTrigger = false;
        _leftArrow.SetActive(false);

        _rightDoor.isTrigger = false;
        _rightArrow.SetActive(false);

        _bottomDoor.isTrigger = false;
        _bottomArrow.SetActive(false);
    }
}