using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public abstract class RotateView: SignalView
    {
        private readonly Transform _transform;
        
        private readonly RotateModel _model;
        private readonly RotatePresenter _presenter;

        private readonly float _angle;

        private Tween _rotateAnimation;
        
        protected abstract ButtonElement _buttonElement { get; set; }

        public RotateView(Transform transform,float angle, 
            RotateModel model, RotatePresenter presenter,
            IIdentifier identifier, SignalBus signalBus) 
            : base(identifier, signalBus)
        {
            _transform = transform;
            
            _model = model;
            _presenter = presenter;

            _angle = angle;
            
        }

        protected override void SetupSignals()
        {
            SignalBus.Subscribe<SelectSignal>(s =>
                    _buttonElement.SetActive(InstanceId,  s.SelectedId == InstanceId, OnClick));
        }

        protected override void SetupObserves()
        {
            SetupRotateObserve();
        }

        private void SetupRotateObserve() => _model.NextAngle
            .ObserveEveryValueChanged(a => a.Value)
            .Subscribe(nextAngle =>
            {
                _rotateAnimation?.Kill();
                
                Vector3 to = _presenter.Direction * nextAngle;

                _rotateAnimation =
                    _transform.DORotate(to, _presenter.RotateDuration);

            }).AddTo(_disposable);

        
        private void OnClick()
        {
            if(!_rotateAnimation.IsActive())
            {
                _presenter.DoRotate(_angle);
            }
        }
    }
}