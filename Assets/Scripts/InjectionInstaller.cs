using Payloads;
using Zenject;

public class InjectionInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PayloadManager>().AsSingle().NonLazy();
    }
}

// public class SettingsInstaller : ScriptableObjectInstallerBase
// {
//     public override void InstallBindings()
//     {
//     }
// }