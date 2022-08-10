using Zenject;

namespace ATG.TableDrop
{
    public class ItemViewFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<UnityEngine.Object, ItemView, ItemView.Factory>()
                .FromFactory<PrefabFactory<ItemView>>();
        }
    }
}