using System;
using ATG.TableDrop.Data;
using DG.Tweening;
using UnityEngine;

namespace ATG.TableDrop
{
    public class TransformPresenter
    {
        private readonly TransformModel _model;
        private readonly ItemTransformData _data;

        public Func<Transform, Tween> SelectAnimation;
        public Func<Transform, Tween> UnselectAnimation;

        public TransformPresenter(TransformModel model, ItemTransformData data)
        {
            _model = model;
            _data = data;
        }

        public void OnTransformInstance(Vector3 instancePosition)
        {
            float startHeight = instancePosition.y;
            float endHeight = startHeight + _data.JumpHeight;
            
            SelectAnimation = transform =>
                transform.DOMoveY(endHeight, _data.JumpDuration)
                    .OnUpdate(() => UpdateHeight(transform.position.y));
            
            UnselectAnimation = transform => 
                transform.DOMoveY(startHeight, _data.JumpDuration)
                    .OnUpdate(() => UpdateHeight(transform.position.y));
            
            _model.Position.Value = instancePosition;
        }
        

        public void OnSelect() => _model.Selection.Value = SelectionType.Select;
        public void OnDeselect() => _model.Selection.Value = SelectionType.Unselect;
        
        public void OnMove(Vector2 direction)
        {
            var curPos = _model.Position.Value;
            var endDir = curPos + new Vector3(direction.x, 0f, direction.y);
            
            _model.Position.Value =
                Vector3.MoveTowards(curPos, endDir, 
                    _data.MoveSpeed * Time.deltaTime);
        }

        private void UpdateHeight(float curHeight)
        {
           var cur = _model.Position.Value;
           cur.y = curHeight;

           _model.Position.Value = cur;
        }
    }
}