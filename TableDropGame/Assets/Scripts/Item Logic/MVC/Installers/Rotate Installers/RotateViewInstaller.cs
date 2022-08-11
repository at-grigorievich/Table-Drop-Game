using ATG.TableDrop.Data;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public abstract class RotateViewInstaller<T>: MonoInstaller 
        where T: RotateView
    {
        [SerializeField] private ItemTransformData _data;
        [SerializeField] private Transform _rotateTransform;
        [Space(15)] [SerializeField] private Vector3 _direction;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<T>()
               .FromSubContainerResolveAll()
                .ByMethod(InstallFacade)
                .AsSingle().NonLazy();
        }

        void InstallFacade(DiContainer subContainer)
        {
            RotateModel rm = new RotateModel();
            subContainer.Bind<Transform>().FromInstance(_rotateTransform).AsSingle();
            subContainer.Bind<RotateModel>().To<RotateModel>().FromInstance(rm).AsSingle();
            subContainer.Bind<RotatePresenter>().FromInstance(new RotatePresenter(rm,_data,_direction)).AsSingle();

            subContainer.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }
    }
}