using UnityEngine;

namespace Project1
{
    public class GridController : MonoBehaviour
    {
        public int grid_N;
        public Grid gridPrefab;
        public Grid[,] _allGridElements;
        
        public static readonly Vector2Int[] NeighborPivots =
        {
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1)
        };

        private void Start()
        {
            CreateGrid();
        }

        public void CreateGrid()
        {
            _allGridElements = new Grid[grid_N, grid_N];
            var offset = grid_N / 2f - 0.5f;

            for (var i = 0; i < grid_N; i++)
            {
                for (var j = 0; j < grid_N; j++)
                {
                    var currentGrid = Instantiate(gridPrefab, new Vector3(i - offset, j - offset),
                        Quaternion.identity, transform);
                    _allGridElements[i, j] = currentGrid;
                    currentGrid.SetNeighbors(new Vector2Int(i, j), grid_N);
                }
            }
        }
    }
}