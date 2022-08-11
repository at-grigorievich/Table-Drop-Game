using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ATG.TableDrop
{
    [RequireComponent(typeof(Button))]
    public class ButtonElement : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        
        private GameObject _gameObject;
       
        private int selectedInstance;
        private bool _isAvailable = true;
        
        public Button Button { get; private set; }
        
        private void Awake()
        {
            Button = GetComponent<Button>();
            _gameObject = gameObject;
            
            SetActive(selectedInstance,false);
            
            _signalBus.Subscribe<BoolSignal>(b => _isAvailable = b.Value);
        }
        
        public void SetActive(int instanceId, bool active, Action onClick = null)
        {
            if (active)
            {
                _gameObject.SetActive(active);
                selectedInstance = instanceId;

                Button.onClick.RemoveAllListeners();
                Button.onClick.AddListener(() => {
                    if (!_isAvailable) return;
                    
                    onClick?.Invoke();
                    onClick?.Invoke();
                });
            }
            else if (selectedInstance == instanceId)
            {
                _gameObject.SetActive(active);
                _isAvailable = true;
            }
        }
    }
}