using UniRx;
using UnityEngine;

namespace ATG.TableDrop
{
    public class ItemModel
    {
        public ReactiveProperty<Vector3> Position { get; set; }

        public ItemModel()
        {
            Position = new ReactiveProperty<Vector3>(Vector3.zero);
        }
    }
}