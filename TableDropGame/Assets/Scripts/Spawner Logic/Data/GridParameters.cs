using UnityEngine;

namespace ATG.TableDrop
{
    [CreateAssetMenu(fileName = "Grid Values", menuName = "Spawner/New Grid Values", order = 0)]
    public sealed class GridParameters : ScriptableObject
    {
        [field: SerializeField] public byte WidthCellCount { get; private set; }
        [field: SerializeField] public byte HeightCellCount { get; private set; }
        [field: SerializeField] public Vector2 CellSize { get; private set; }
        
        [field: Range(0,1f)]
        [field: SerializeField] public float FaultValue { get; private set; }

    }
}