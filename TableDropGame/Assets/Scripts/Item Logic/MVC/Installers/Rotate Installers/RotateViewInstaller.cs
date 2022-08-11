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
            RotateModel rm = new RotateModel();
            Container.BindInstance(_rotateTransform).AsSingle();
            Container.Bind<RotateModel>().FromInstance(rm).AsSingle();
            Container.
                Bind<RotatePresenter>().FromInstance(new RotatePresenter(rm,_data,_direction)).
                AsSingle();
            
            Container.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }
    }
}