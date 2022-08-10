#if UNITY_EDITOR
using UnityEngine;

    namespace ATG.TableDrop
    {
        public class SpawnerServiceVisualize : MonoBehaviour
        {
            [SerializeField] private SpawnerParameters _spawnerParameters;
            [Space(15)] 
            [SerializeField] private Transform _spawnerTransform;
            
            private void OnDrawGizmos()
            {
                var xSize = _spawnerParameters.GridValue.CellSize.x;
                var zSize = _spawnerParameters.GridValue.CellSize.y;
                
                var xCount = _spawnerParameters.GridValue.WidthCellCount;
                var zCount = _spawnerParameters.GridValue.HeightCellCount;

                Gizmos.color = new Color(1, 0, 0, 0.5f);

                var spawnerPosition =
                    _spawnerTransform.position;
                
                for (int i = 0; i < xCount; i++)
                {
                    for (int j = 0; j < zCount; j++)
                    {
                        var curPos = spawnerPosition + new Vector3(xSize * i, 0f, - zSize* j);
                        
                        Gizmos.DrawCube(curPos+Vector3.up*0.1f,new Vector3(xSize*0.95f,0.1f,zSize*0.95f));
                    }
                }
            }
        }
    }
#endif