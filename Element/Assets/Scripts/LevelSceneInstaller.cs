using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] Boss _bossPrefab;
    [SerializeField] EnvironmentManager _environmentManager;
    [SerializeField] Room _room;
    [SerializeField] EraserManager _eraserManager;
    [SerializeField] UIManager _uiManager;
    public override void InstallBindings()
    {
        Container.Bind<Boss>().FromInstance(_bossPrefab);
        Container.Bind<EnvironmentManager>().FromInstance(_environmentManager);
        Container.Bind<Room>().FromInstance(_room);
        Container.Bind<EraserManager>().FromInstance(_eraserManager);
        Container.Bind<UIManager>().FromInstance(_uiManager);
    }
}