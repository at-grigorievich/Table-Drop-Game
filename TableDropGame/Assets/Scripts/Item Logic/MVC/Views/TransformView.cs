﻿using UniRx;
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
            IIdentifier item, SignalBus bus) : base(item,bus)
        {
            _model = model;
            _presenter = presenter;
            
            _transform = transform;
            
        }
        
        protected override void SetupSignals()
        {
            SignalBus.Subscribe<InitPositionSignal>(p =>
            {
                if (p.Id != InstanceId) return;
                _transform.SetParent(p.Parent);
                _presenter.OnTransformInstance(p.Position);
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
                .Subscribe(pos => _transform.position = pos)
                .AddTo(_disposable);
         
        #endregion
    }
}