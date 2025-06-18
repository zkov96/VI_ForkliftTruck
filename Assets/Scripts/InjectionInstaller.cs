using ForkLift;
using Payloads;
using UnityEngine;
using Zenject;

public class InjectionInstaller : MonoInstaller
{
    [SerializeField] private ForkliftConfig _config;
    [SerializeField] private ForkLift.ForkLift _forklift;
    
    public override void InstallBindings()
    {
        // Container.Bind<DiContainer>().FromInstance(Container);
        Container.Bind<PayloadManager>().AsSingle().NonLazy();
        Container.Bind<ForkLift.ForkLift>().FromInstance(_forklift);
        Container.Bind<ForkliftConfig>().FromInstance(_config);
    }
}

// public class SettingsInstaller : ScriptableObjectInstallerBase
// {
//     public override void InstallBindings()
//     {
//     }
// }