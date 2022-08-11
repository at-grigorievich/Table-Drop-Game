using System;
using UnityEngine;
using UnityEngine.UI;

namespace ATG.TableDrop
{
    [RequireComponent(typeof(Button))]
    public class ButtonElement : MonoBehaviour
    {
        public Button Button { get; private set; }
        private GameObject _gameObject;

        private int selectedInstance;
        
        private void Awake()
        {
            Button = GetComponent<Button>();
            _gameObject = gameObject;
            
            SetActive(selectedInstance,false);
        }



        public void SetActive(int instanceId, bool active, Action onClick = null)
        {
            if (active)
            {
                _gameObject.SetActive(active);
                selectedInstance = instanceId;

                Button.onClick.RemoveAllListeners();
                Button.onClick.AddListener(() => onClick?.Invoke());
            }
            else if (selectedInstance == instanceId)
            {
                _gameObject.SetActive(active);
            }
        }
    }
}