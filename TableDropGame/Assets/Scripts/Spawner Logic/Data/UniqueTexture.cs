using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ATG.TableDrop
{
    [CreateAssetMenu(fileName = "Texture Data", menuName = "Spawner/New Texture Data", order = 0)]
    public class UniqueTexture: ScriptableObject
    {
        [SerializeField] private string _textureName;
        [Space(10)] 
        [SerializeField] private bool _isLoadFromWWW;
        
        public async UniTask<Texture2D> LoadTexture()
        {
            return !_isLoadFromWWW
                ? await DownloadFromLocal($"{Application.dataPath}/StreamingAssets/Sprites/{_textureName}.png")
                : await DownloadFromWWW(_textureName);
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
}