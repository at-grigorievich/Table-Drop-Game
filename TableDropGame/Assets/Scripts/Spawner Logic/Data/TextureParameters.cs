using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ATG.TableDrop
{
    [CreateAssetMenu(fileName = "Texture Data", menuName = "Spawner/New Texture Data", order = 0)]
    public class UniqueTexture: ScriptableObject
    {
        [SerializeField] private string textureName;
        [Space(10)] 
        [SerializeField] private bool _isLoadFromWWW;
        
        public async UniTask<Texture2D> LoadTexture()
        {
            return !_isLoadFromWWW
                ? await DownloadFromLocal($"{Application.dataPath}/StreamingAssets/Sprites/{textureName}.png")
                : await DownloadFromWWW(textureName);
        }

        private static async UniTask<Texture2D> DownloadFromLocal(string path)
        {
            var file = new FileInfo(path);
            byte[] bytes;
            
            if (file.Exists)
            {
                bytes = await File.ReadAllBytesAsync(path);

                var texture = new Texture2D(1, 1);
                texture.LoadImage(bytes);
                
                return texture;
            }
            throw new NullReferenceException("Cant find path " + path);
        }
        private static async UniTask<Texture2D> DownloadFromWWW(string path)
        {
            using var request = UnityWebRequestTexture.GetTexture(path);

            await request.SendWebRequest();
            
            return  request.result == UnityWebRequest.Result.Success
                ? DownloadHandlerTexture.GetContent(request) : null;
        }
    }
    
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