using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ATG.TableDrop
{
    public class InputService: IInitializable
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        private readonly PlayerInput _input;
        private readonly SignalBus _bus;

        private readonly Camera _camera;
        
        private ReactiveProperty<IIdentifier> _selectedIdentifier;

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
                .AddTo(_disposable);

            StartUpdate();
        }

        private void StartUpdate()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (_selectedIdentifier.Value == null) return;
                    
                    Vector2 input = _input.Player.Move.ReadValue<Vector2>();
                    _bus.TryFire(new MoveSignal(_selectedIdentifier.Value.InstanceId, input));
                })
                .AddTo(_disposable);
        }
        
        private void TrySelect(InputAction.CallbackContext c)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(
                _input.Player.Cursor.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                IIdentifier selected = null;

                if (hit.transform.TryGetComponent(out ItemCollider collider))
                {
                    selected = collider.Identifier;
                }

                _selectedIdentifier.Value = selected;
            }
        }
        
    }
}