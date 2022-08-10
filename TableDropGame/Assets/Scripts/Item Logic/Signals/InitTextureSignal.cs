using UnityEngine;

namespace ATG.TableDrop
{
    public struct InitTextureSignal
    {
        public readonly int Id;
        public readonly Texture2D Texture;

        public InitTextureSignal(int id, Texture2D texture)
        {
            Id = id;
            Texture = texture;
        }
    }
}