using ATG.TableDrop.Data;
using UnityEngine;

namespace ATG.TableDrop
{
    public class RotatePresenter
    {
        private readonly RotateModel _model;
        private readonly ItemTransformData _data;

        public readonly Vector3 Direction;
        
        public float RotateDuration => 1 / _data.RotateSpeed;

        public RotatePresenter(RotateModel model,ItemTransformData data, Vector3 direction)
        {
            _model = model;
            _data = data;

            Direction = direction.normalized;
        }

        public void DoRotate(float baseAngle) => _model.NextAngle.Value += baseAngle;
    }
}