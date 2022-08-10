using System.Collections.Generic;
using UnityEngine;

namespace ATG.TableDrop
{
    [CreateAssetMenu(fileName = "Spawner Data", menuName = "Spawner/New Spawner Values", order = 0)]
    public sealed class SpawnerParameters : ScriptableObject
    {
        [SerializeField] private int _countPerPrefab;
        [SerializeField] private ItemView[] _itemViewsPrefabs;
        [SerializeField] private GridParameters _gridParameters;
        
        private HashSet<ItemView> _items;

        public HashSet<ItemView> ItemPrefabs 
        {
            get
            {
                _items ??= new HashSet<ItemView>(_itemViewsPrefabs);
                return _items;
            }
        }

        public GridParameters GridValue => _gridParameters;
        
        public int CountPerPrefab => _countPerPrefab;
        public int TotalCount => _countPerPrefab*ItemPrefabs.Count;
    }
}