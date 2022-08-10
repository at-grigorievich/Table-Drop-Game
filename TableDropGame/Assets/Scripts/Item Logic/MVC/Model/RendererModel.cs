using UniRx;
using UnityEngine;

namespace ATG.TableDrop
{
    public class RendererModel
    {
        public ReactiveProperty<Color> Color { get; set; }
        public ReactiveProperty<Texture2D> Texture { get; set; }

        public RendererModel()
        {
            Color = new ColorReactiveProperty(UnityEngine.Color.white);
            Texture = new ReactiveProperty<Texture2D>(Texture2D.whiteTexture);
;        }
    }
}