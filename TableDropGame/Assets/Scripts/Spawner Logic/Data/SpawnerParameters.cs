using System.Collections.Generic;
using UnityEngine;

namespace ATG.TableDrop
{
    [CreateAssetMenu(fileName = "Spawner Data", menuName = "Spawner/New Spawner Values", order = 0)]
    public sealed class SpawnerParameters : ScriptableObject
    {
        [SerializeField] private int _totalInstanceCount;
        
        [SerializeField] private ItemObject[] _itemViewsPrefabs;
        
        [SerializeField] private GridParameters _gridParameters;

        private HashSet<ItemObject> _items;

        public HashSet<ItemObject> ItemPrefabs 
        {
            get
            {
                _items ??= new HashSet<ItemObject>(_itemViewsPrefabs);
                return _items;
            }
        }

        public GridParameters GridValue => _gridParameters;
        
        public int CountPerPrefab => TotalCount/ItemPrefabs.Count;
        public int TotalCount => _totalInstanceCount;
    }
}