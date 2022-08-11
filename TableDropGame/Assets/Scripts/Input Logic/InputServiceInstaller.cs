using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class InputServiceInstaller: MonoInstaller
    {
        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_camera).AsSingle();
            Container.Bind<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
        }
    }
}