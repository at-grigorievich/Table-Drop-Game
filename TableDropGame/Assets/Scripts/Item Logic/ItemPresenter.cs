using UnityEngine;

namespace ATG.TableDrop
{
    public class ItemPresenter
    {
        private readonly ItemModel _model;

        public ItemPresenter(ItemModel model)
        {
            _model = model;
        }

        public void DoInstantiate(Vector3 instancePosition) =>
            _model.Position.Value = instancePosition;
    }
}