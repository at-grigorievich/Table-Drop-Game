using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.TableDrop
{
    [CreateAssetMenu(fileName = "Texture Values", menuName = "Spawner/New Texture Set", order = 0)]
    public class TextureParameters : ScriptableObject
    {
        [SerializeField] private UniqueTexture[] _texturesData;

        private Queue<UniqueTexture> _texturesNameQueue;

        private void OnEnable()
        {
            _texturesNameQueue = new Queue<UniqueTexture>(_texturesData.Distinct());
        }

        public async void DoLoadTexture(Action<Texture2D> callback)
        {
            if (_texturesNameQueue.Count < 0)
            {
                throw new ArgumentOutOfRangeException("no available textures");
            }

            Texture2D loadedTexture = await _texturesNameQueue.Dequeue().LoadTexture();
            callback?.Invoke(loadedTexture);
        }
    }
}