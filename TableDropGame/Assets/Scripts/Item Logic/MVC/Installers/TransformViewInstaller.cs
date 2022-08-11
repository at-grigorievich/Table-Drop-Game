using ATG.TableDrop.Data;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public sealed class TransformViewInstaller: MonoInstaller
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ItemTransformData _transformData;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_rigidbody).AsSingle();
            Container.BindInstance(_transformData).AsSingle();
            
            Container.Bind<TransformModel>().AsSingle();
            Container.Bind<TransformPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TransformView>().AsSingle().NonLazy();
        }
    }
}