using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public interface ITransformView
    {
        void OnInstantiate(Vector3 positon, Transform parent);
    }
    
    public class ItemView : MonoBehaviour, ITransformView
    {
        //[Inject] private SignalBus _bus;
        private ItemPresenter _presenter;
        private ItemModel _model;

        private Transform _myTransform;

        private CompositeDisposable _disposable = new CompositeDisposable();
        
        [Inject]
        private void Constructor()
        {
            _myTransform = transform;
            
            _model = new ItemModel();
            _presenter = new ItemPresenter(_model);
            
            SetupPositionObserve();
        }

        #region ITransformView Implementation
        public void OnInstantiate(Vector3 position, Transform parent)
        {
            _myTransform.SetParent(parent);
            _presenter.DoInstantiate(position);
        }
        
        #endregion
        
        private void SetupPositionObserve() =>
            _model.Position
                .ObserveEveryValueChanged(pos => pos.Value)
                .Subscribe(pos => transform.position = pos).AddTo(_disposable);

        
        
        private void OnDisable()
        {
            _disposable.Clear();
        }

        public class Factory: PlaceholderFactory<UnityEngine.Object,ItemView>{}
    }
    
}