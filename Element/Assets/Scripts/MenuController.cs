using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] RectTransform _background;
    int _direction = -1;

    void FixedUpdate()
    {
        _background.position += new Vector3(2, 0, 0) * _direction;
        if (_background.position.x <= -1920 || _background.position.x > 650) _direction = -_direction;
    }

    public void Play()
    {
        SceneManager.LoadScene("Level");
    }
}
