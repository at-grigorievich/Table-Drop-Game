using ATG.TableDrop.Data;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class RendererViewInstaller: MonoInstaller
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private ItemRendererData _rendererData;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_renderer).AsSingle().NonLazy();
            Container.BindInstance(_rendererData).AsSingle();
            
            Container.Bind<RendererModel>().AsSingle();
            Container.Bind<RendererPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<RendererView>().AsSingle().NonLazy();
        }
    }
}