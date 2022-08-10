using Zenject;

namespace ATG.TableDrop
{
    public class ItemObjectFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<UnityEngine.Object, ItemObject, ItemObject.Factory>()
                .FromFactory<PrefabFactory<ItemObject>>();
        }
    }
}