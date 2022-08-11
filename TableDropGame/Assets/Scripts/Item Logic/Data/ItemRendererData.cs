using UnityEngine;

namespace ATG.TableDrop.Data
{
    [CreateAssetMenu(fileName = "Item Renderer Data", menuName = "Item Data/Renderer Data", order = 0)]
    public class ItemRendererData : ScriptableObject
    {
        [field: SerializeField] public Color SelectColor { get; private set; }
        [field: SerializeField] public Color UnselectColor { get; private set; }
        
        [field: Space(15)]
        
        [field: SerializeField] public float SetColorDuration { get; private set; }
    }
}