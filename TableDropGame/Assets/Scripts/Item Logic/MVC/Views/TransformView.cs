using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class TransformView: SignalView
    {
        private readonly Rigidbody _rb;
        private readonly Transform _transform;

        private readonly TransformPresenter _presenter;
        private readonly TransformModel _model;

        private Tween _currentTween;

        public TransformView(Rigidbody rb, 
            TransformPresenter presenter,TransformModel model,
            IIdentifier item, SignalBus bus) : base(item,bus)
        {
            _model = model;
            _presenter = presenter;

            _rb = rb;
            _transform = _rb.transform;
        }

        protected override void SetupSignals()
        {
            SignalBus.Subscribe<InitPositionSignal>(p =>
            {
                if (p.Id != InstanceId) return;
                _transform.SetParent(p.Parent);
                _presenter.OnTransformInstance(p.Position);

                Observable.EveryUpdate()
                    .Subscribe(e => _model.Position.Value = _transform.position)
                    .AddTo(_disposable);
            });
            
            SignalBus.Subscribe<SelectSignal>(s =>
            {
                if (s.SelectedId == InstanceId)
                {
                    _presenter.OnSelect();
                    _rb.isKinematic = true;
                }
                else
                {
                    _rb.isKinematic = false;
                    _presenter.OnDeselect();
                }
            });
            
            SignalBus.Subscribe<MoveSignal>(m =>
            {
                if (m.Id == InstanceId)
                {
                    _presenter.OnMove(m.MoveDirection);
                }
            });
        }
        protected override void SetupObserves()
        {
            SetupPositionObserve();
            SetupSelectObserve();
        }
        
        #region Observes
         
        private void SetupPositionObserve() =>
            _model.Position
                .ObserveEveryValueChanged(pos => pos.Value)
                .Subscribe(pos => _transform.position = pos)
                .AddTo(_disposable);

        private void SetupSelectObserve() =>
            _model.Selection
                .ObserveEveryValueChanged(s => s.Value)
                .Subscribe(t =>
                {
                    _currentTween?.Kill();

                    switch (t)
                    {
                        case SelectionType.Select:
                            _currentTween = _presenter.SelectAnimation(_transform);
                            break;
                        case SelectionType.Unselect:
                            _currentTween = _presenter.UnselectAnimation(_transform);
                            break;
                    }

                    _currentTween.Play();
                })
                .AddTo(_disposable);

        #endregion
    }
}