using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] Boss _bossPrefab;
    [SerializeField] EnvironmentManager _environmentManager;
    public override void InstallBindings()
    {
        Container.Bind<Boss>().FromInstance(_bossPrefab);
        Container.Bind<EnvironmentManager>().FromInstance(_environmentManager);
    }
}