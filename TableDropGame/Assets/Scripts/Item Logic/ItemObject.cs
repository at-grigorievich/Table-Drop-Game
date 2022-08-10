using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class ItemObject : MonoBehaviour, IIdentifier
    {
        public int InstanceId => gameObject.GetInstanceID();

        public class Factory: PlaceholderFactory<UnityEngine.Object,ItemObject>{}
    }
    
}