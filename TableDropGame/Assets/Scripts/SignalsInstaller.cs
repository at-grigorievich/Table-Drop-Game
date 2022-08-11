using Zenject;

namespace ATG.TableDrop
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<InitPositionSignal>();
            Container.DeclareSignal<InputPositionSignal>();

            Container.DeclareSignal<InitTextureSignal>();

            Container.DeclareSignal<SelectSignal>();
            Container.DeclareSignal<MoveSignal>();

            Container.DeclareSignal<BoolSignal>();
        }
    }
}
