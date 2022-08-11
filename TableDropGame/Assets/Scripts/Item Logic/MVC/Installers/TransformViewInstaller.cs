using ATG.TableDrop.Data;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public sealed class TransformViewInstaller: MonoInstaller
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private ItemTransformData _transformData;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_transform).AsSingle();
            Container.BindInstance(_transformData).AsSingle();
            
            Container.Bind<TransformModel>().AsSingle();
            Container.Bind<TransformPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TransformView>().AsSingle().NonLazy();
        }
    }
}