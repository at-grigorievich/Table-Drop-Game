using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class ItemObjectInstaller: MonoInstaller
    {
        [SerializeField] private ItemObject _itemObject;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ItemObject>().FromInstance(_itemObject)
                .AsSingle().NonLazy();
        }
    }
}