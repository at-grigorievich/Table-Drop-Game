using System;
using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    [RequireComponent(typeof(Rigidbody))]
    public class ItemObject : MonoBehaviour, IIdentifier
    {
        public int InstanceId => gameObject.GetInstanceID();
        

        public class Factory: PlaceholderFactory<UnityEngine.Object,ItemObject>{}
    }
    
}