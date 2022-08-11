using ATG.TableDrop.Data;
using UnityEngine;

namespace ATG.TableDrop
{
    public class RendererPresenter
    {
        private readonly RendererModel _model;
        private readonly ItemRendererData _data;

        public float AnimateColorDuration => _data.SetColorDuration;
        
        public RendererPresenter(RendererModel model, ItemRendererData data)
        {
            _model = model;
            _data = data;
        }

        public void OnSetupTexture(Texture2D texture) 
            => _model.Texture.Value = texture;

        public void OnSelect() => _model.Color.Value = _data.SelectColor;
        public void OnDeselect() => _model.Color.Value = _data.UnselectColor;

    }
}