using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public enum SelectionType
    {
        None,
        Select,
        Unselect
    }
    
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
            SignalBus.Subscribe<InitPositionSignal>(p => {
                if (p.Id != InstanceId) return;
                _transform.SetParent(p.Parent);
                _presenter.OnTransformInstance(p.Position);
            });
            SignalBus.Subscribe<SelectSignal>(s => {
                if (s.SelectedId == InstanceId)
                {
                    _presenter.OnSelect();
                    
                    _rb.transform.rotation = Quaternion.identity;
                    _rb.isKinematic = true;
                }
                else
                {
                    _rb.isKinematic = false;
                    _presenter.OnDeselect();
                }
            });
            SignalBus.Subscribe<MoveSignal>(m => {
                if (m.Id == InstanceId) 
                    _presenter.OnMove(m.MoveDirection);
            });
        }
        protected override void SetupObserves()
        {
            SetupInitPositionObserve();
            SetupSelectObserve();
            SetupRigidbodyMoveObserve();
        }
        
        #region Observes
         
        private void SetupInitPositionObserve() =>
            _model.Position
                .ObserveEveryValueChanged(pos => pos.Value)
                .Subscribe(pos => _transform.position = pos)
                .AddTo(_transform);

        private void SetupRigidbodyMoveObserve() =>
            _model.Direction
                .ObserveEveryValueChanged(pos => pos.Value)
                .Subscribe(dir => _rb.MovePosition(_transform.position + dir))
                .AddTo(_transform);
        
        private void SetupSelectObserve() =>
            _model.Selection
                .ObserveEveryValueChanged(s => s.Value)
                .Subscribe(t => {
                    _currentTween?.Kill();

                    _currentTween = t switch
                    {
                        SelectionType.Select => _presenter.SelectAnimation(_transform),
                        _ => _currentTween
                    };

                    _currentTween.Play();
                })
                .AddTo(_transform);

        #endregion
    }
}