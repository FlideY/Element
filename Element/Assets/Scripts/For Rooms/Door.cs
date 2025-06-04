using UnityEngine;

public class Door : MonoBehaviour
{
    RoomManager _roomManager;
    void Start()
    {
        _roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player")
        {
            Vector2Int direction = new();
            Vector2 posChange = new();
            switch (tag)
            {
                case "TopDoor":
                    direction = new(0, 1);
                    posChange = new(0, -43);
                    break;
                case "RightDoor":
                    direction = new(1, 0);
                    posChange = new(-60, 0);
                    break;
                case "LeftDoor":
                    direction = new(-1, 0);
                    posChange = new(60, 0);
                    break;
                case "BottomDoor":
                    direction = new(0, -1);
                    posChange = new(0, 43);
                    break;
            }

            _roomManager.SwitchRoom(direction);
            other.transform.position += new Vector3(posChange.x, posChange.y, 0);
        }
    }
}