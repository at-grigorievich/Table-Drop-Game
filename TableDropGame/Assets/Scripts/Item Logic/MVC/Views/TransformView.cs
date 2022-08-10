using UniRx;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class TransformView: SignalView
    {
        private readonly Transform _transform;

        private readonly TransformPresenter _presenter;
        private readonly TransformModel _model;
        
        
        public TransformView(Transform transform, 
            TransformPresenter presenter,TransformModel model,
            int instanceId, SignalBus bus) : base(instanceId,bus)
        {
            _model = model;
            _presenter = presenter;
            
            _transform = transform;
            
        }
        
        protected override void SetupSignals()
        {
            SignalBus.Subscribe<InitPositionSignal>(p =>
            {
                if (p.Id == InstanceId)
                {
                    _transform.SetParent(p.Parent);
                    _presenter.OnTransformInstance(p.Position);
                }
            });
            
        }

        protected override void SetupObserves()
        {
            SetupPositionObserve();
        }
        
        #region Observes
         
        private void SetupPositionObserve() =>
            _model.Position
                .ObserveEveryValueChanged(pos => pos.Value)
                .Subscribe(pos => _transform.position = pos);
         
        #endregion
    }
}