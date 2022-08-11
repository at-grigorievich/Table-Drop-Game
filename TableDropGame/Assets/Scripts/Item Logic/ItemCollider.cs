using UnityEngine;

namespace ATG.TableDrop
{
    [RequireComponent(typeof(Collider))]
    public class ItemCollider: MonoBehaviour
    {
        [SerializeField] private ItemObject _item;

        public IIdentifier Identifier => _item;
    }
}
