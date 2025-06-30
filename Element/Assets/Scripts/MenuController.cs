using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] RectTransform _background;
    int _direction = -1;
    float _timer = 26;
    float _coolDown = 26;

    void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;
        if (_timer > 0)
        {
            _background.position += new Vector3(2, 0, 0) * _direction;
        }
        else
        {
            _timer = _coolDown;
            _direction = -_direction;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Levels", LoadSceneMode.Additive);
    }
}
