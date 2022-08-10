using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class RendererViewInstaller: MonoInstaller
    {
        [SerializeField] private Renderer _renderer;

        public override void InstallBindings()
        {
            Container.BindInstance(_renderer).AsSingle().NonLazy();

            Container.Bind<RendererModel>().AsSingle();
            Container.Bind<RendererPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<RendererView>().AsSingle().NonLazy();
        }
    }
}