using UnityEngine;

namespace ATG.TableDrop
{
    public class TransformPresenter
    {
        private readonly TransformModel _model;
        
        public TransformPresenter(TransformModel model)
        {
            _model = model;
        }

        public void OnTransformInstance(Vector3 instancePosition) =>
            _model.Position.Value = instancePosition;
    }
}