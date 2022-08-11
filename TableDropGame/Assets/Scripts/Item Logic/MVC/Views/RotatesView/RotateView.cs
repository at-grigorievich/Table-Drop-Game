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
            .Subscribe(nextAngle => {
                if(nextAngle == 0)
                    return;
                
                _rotateAnimation?.Kill();
                var to = _presenter.Direction * _angle + _transform.eulerAngles;

                _rotateAnimation =
                    _transform.DORotateQuaternion(Quaternion.Euler(to), _presenter.RotateDuration)
                        .OnComplete(() => SignalBus.TryFire(new BoolSignal(true)));

            })
            .AddTo(_disposable);

        
        private void OnClick()
        {
            SignalBus.TryFire(new BoolSignal(false));
            _presenter.DoRotate(_angle);
        }
    }
}