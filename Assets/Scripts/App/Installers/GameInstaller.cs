using Dan398.Time.Controller;
using Dan398.Time.Network;
using Dan398.Time.View;
using Dan398.Time;
using Zenject;

namespace Dan398.App.Installers
{
    public sealed class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IServerTimeProvider>()
                .FromMethod(_ => new FallbackTimeProvider(new IServerTimeProvider[]
                {
                    new YandexTimeProvider(),
                    new TimeApiTimeProvider(),
                    new LocalMachineTimeProvider()
                }))
                .AsSingle();

            Container.BindInterfacesAndSelfTo<AppClock>().AsSingle();
            Container.BindInterfacesTo<TimeSyncController>().AsSingle();
            Container.BindInterfacesTo<ConsoleClockView>().AsSingle();
        }
    }
}