using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] GameObject[] _environmentConfigurations;

    public void SetEnvironment(int index)
    {
        foreach (GameObject config in _environmentConfigurations)
        {
            config.SetActive(false);
        }
        if(index >= 0 && index <= _environmentConfigurations.Length)
            _environmentConfigurations[index].SetActive(true);
    }
}
