using DG.Tweening;
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

        private Tween _tween;
        
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
            
            SignalBus.Subscribe<SelectSignal>(s =>
            {
                if (s.SelectedId == InstanceId) _presenter.OnSelect();
                else _presenter.OnDeselect();
            });
        }
        protected override void SetupObserves()
        {
            SetupTextureObserve();
            SetupColorObserve();
        }

        private void SetupTextureObserve() =>
            _model.Texture
                .ObserveEveryValueChanged(t => t.Value)
                .Subscribe(texture => _renderer.material.mainTexture = texture)
                .AddTo(_disposable);

        private void SetupColorObserve() =>
            _model.Color
                .ObserveEveryValueChanged(c => c.Value)
                .Subscribe(color =>
                {
                    _tween?.Kill();
                    _tween = _renderer.material.DOColor(color, _presenter.AnimateColorDuration);
                    _tween.Play();
                })
                .AddTo(_disposable);

    }
}