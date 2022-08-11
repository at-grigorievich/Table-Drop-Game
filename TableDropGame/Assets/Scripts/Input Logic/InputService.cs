using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace ATG.TableDrop
{
    public class InputService: IInitializable
    {
        private readonly PlayerInput _input;
        private readonly SignalBus _bus;

        private readonly Camera _camera;
        
        private ReactiveProperty<IIdentifier> _selectedIdentifier;

        private bool _pointerOverUI;
        
        public InputService(Camera camera,PlayerInput input, SignalBus bus)
        {
            _input = input;
            _bus = bus;

            _camera = camera;
        }

        public void Initialize()
        {
            _input.Enable();
            _input.Player.Select.performed += TrySelect;
            
            _selectedIdentifier = new ReactiveProperty<IIdentifier>(null);
            
            _selectedIdentifier
                .ObserveEveryValueChanged(e => e.Value)
                .Subscribe(id => _bus.TryFire(new SelectSignal(id)))
                .AddTo(_camera);

            StartUpdate();
        }

        private void StartUpdate()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    _pointerOverUI = EventSystem.current.IsPointerOverGameObject();
                    
                    if (_selectedIdentifier.Value == null) return;
                    
                    Vector2 input = _input.Player.Move.ReadValue<Vector2>();
                    _bus.TryFire(new MoveSignal(_selectedIdentifier.Value.InstanceId, input));
                })
                .AddTo(_camera);
        }
        
        private void TrySelect(InputAction.CallbackContext c)
        {
            var ray = _camera.ScreenPointToRay(
                _input.Player.Cursor.ReadValue<Vector2>());
            
            if (_pointerOverUI) return;

            IIdentifier selected = null;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                hit.transform.TryGetComponent(out selected);
            }

            _selectedIdentifier.Value = selected;
        }
    }
}