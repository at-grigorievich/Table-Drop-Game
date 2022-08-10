using UnityEngine;

namespace ATG.TableDrop
{
    public class RendererPresenter
    {
        private readonly RendererModel _model;
        
        public RendererPresenter(RendererModel model)
        {
            _model = model;
        }

        public void OnSetupTexture(Texture2D texture) 
            => _model.Texture.Value = texture;
        
        //public void OnChangeColor(bool isSelect) =>
            
    }
}