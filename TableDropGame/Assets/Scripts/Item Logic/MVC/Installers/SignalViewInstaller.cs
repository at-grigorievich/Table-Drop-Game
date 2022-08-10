using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public sealed class SignalViewInstaller: MonoInstaller
    {
        [SerializeField] protected ItemObject _root;

        public override void InstallBindings()
        {
            Container.BindInstance(_root.InstanceId).AsSingle().NonLazy();
        }
    }
}