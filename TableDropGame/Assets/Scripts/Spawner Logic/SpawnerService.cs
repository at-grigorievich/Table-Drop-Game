using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ATG.TableDrop
{
    public struct Grid
    {
        public readonly Queue<Vector3> GenerateCells;

        private Vector3[] _gridCells;
        
        public Grid(Vector3 leftCorner,GridParameters gridParameters,int needCellCount)
        {
            GenerateCells = new Queue<Vector3>();
            
            var xSize = gridParameters.CellSize.x;
            var zSize = gridParameters.CellSize.y;
                
            var xCount = gridParameters.WidthCellCount;
            var zCount = gridParameters.HeightCellCount;
            
           _gridCells = new Vector3[xCount * zCount];

            float fault = gridParameters.FaultValue;
            int count = 0;
            
            for (var i = 0; i < xCount; i++)
            {
                for (var j = 0; j < zCount; j++)
                {
                    Vector3 rnd = new Vector3(Random.value, 0f, Random.value) * fault;
                    var curPos =
                        leftCorner + 
                        new Vector3(xSize * i, 0f, - zSize* j) + rnd;
                    
                    _gridCells[count] = curPos;
                    
                    count++;
                }
            }
            
            GenerateAvailableCells(needCellCount);
        }

        private void GenerateAvailableCells(int needCellCount)
        {
            int arrCount = _gridCells.Length;

            while (GenerateCells.Count < needCellCount)
            {
                var rnd = Random.Range(0, arrCount);
                var selected = _gridCells[rnd];
                
                if(!GenerateCells.Contains(selected))
                    GenerateCells.Enqueue(selected);
            }
        }
    }
    
    public class SpawnerService: MonoBehaviour
    {
        [SerializeField] private Transform _spawnerLeftCorner;
        [SerializeField] private SpawnerParameters _spawnerData;
        [SerializeField] private TextureParameters _textureParameters;

        private Grid _grid;

        [Inject]
        private void Constructor(ItemObject.Factory factory, SignalBus bus)
        {
            _grid = new Grid(_spawnerLeftCorner.position, 
                _spawnerData.GridValue, _spawnerData.TotalCount);
            
            InstantiateViews(factory,bus);
        }

        private void InstantiateViews(ItemObject.Factory factory, SignalBus bus)
        {
            Transform parent = new GameObject("Items Container").transform;
            
            int cellPerInstance = _spawnerData.CountPerPrefab;
            
            foreach (var prefab in _spawnerData.ItemPrefabs)
            {
                for (var j = 0; j < cellPerInstance; j++)
                {
                    Vector3 selectedCell = _grid.GenerateCells.Dequeue();
                    
                    var instance = factory.Create(prefab);

                    bus.TryFire(new InitPositionSignal(
                        instance.InstanceId,parent,selectedCell));
                    
                    _textureParameters.DoLoadTexture(t => 
                        bus.TryFire(new InitTextureSignal(instance.InstanceId, t)));
                }
            }
        }
    }
}