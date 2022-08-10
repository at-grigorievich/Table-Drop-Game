using UniRx;
using UnityEngine;

namespace ATG.TableDrop
{
    public class TransformModel
    {
        public ReactiveProperty<Vector3> Position { get; set; }

        public TransformModel()
        {
            Position = new ReactiveProperty<Vector3>(Vector3.zero);
        }
    }
}