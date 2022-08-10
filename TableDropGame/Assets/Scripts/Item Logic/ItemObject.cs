using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class ItemObject : MonoBehaviour
    {
        public int InstanceId => gameObject.GetInstanceID();
        public class Factory: PlaceholderFactory<UnityEngine.Object,ItemObject>{}
    }
    
}