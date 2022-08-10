using UniRx;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class RendererView: SignalView
    {
        private readonly Renderer _renderer;

        private readonly RendererPresenter _presenter;
        private readonly RendererModel _model;
        
        public RendererView(Renderer renderer,
            RendererPresenter presenter, RendererModel model,
            IIdentifier item, SignalBus signalBus) 
            : base(item, signalBus)
        {
            _presenter = presenter;
            _model = model;

            _renderer = renderer;
        }

        protected override void SetupSignals()
        {
            SignalBus.Subscribe<InitTextureSignal>(t =>
            {
                if(t.Id != InstanceId) return;
                _presenter.OnSetupTexture(t.Texture);
            });
        }
        protected override void SetupObserves()
        {
            SetupTextureObserve();
        }

        private void SetupTextureObserve() =>
            _model.Texture
                .ObserveEveryValueChanged(t => t.Value)
                .Subscribe(texture => _renderer.material.mainTexture = texture)
                .AddTo(_disposable);

    }
}