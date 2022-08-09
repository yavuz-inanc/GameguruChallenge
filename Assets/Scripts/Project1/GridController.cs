using System.Collections.Generic;
using UnityEngine;

namespace Project1
{
    public class GridController : MonoBehaviour
    {
        public Grid gridPrefab;
        public int gridN;
        public Grid[,] grids;
        public int GridCount => gridN * gridN;
        
        public List<Grid> neighborGrids = new List<Grid>();
        public int count;
        
        public readonly Vector2Int[] neighborPivots =
        {
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1)
        };

        private void Start()
        {
            CreateGrid();
            SetNeighbors();
        }

        public void CreateGrid()
        {
            grids = new Grid[gridN, gridN];
            var offset = gridN / 2f - 0.5f;
            
            for (var i = 0; i < GridCount; i++)
            {
                var gridIndex = CalculateGridIndex(i);
                
                var xPosition = gridIndex.x - offset;
                var yPosition = offset - gridIndex.y;
                
                var currentGrid = Instantiate(gridPrefab, new Vector3(xPosition, yPosition, 0f),
                    Quaternion.identity, transform);
                
                grids[gridIndex.x, gridIndex.y] = currentGrid;
            }
        }

        public void SetNeighbors()
        {
            for (int i = 0; i < GridCount; i++)
            {
                var index = CalculateGridIndex(i);
                var currentGrid = grids[index.x, index.y];
                currentGrid.neighbors.Clear();
                
                for (int j = 0; j < neighborPivots.Length; j++)
                {
                    var currentNeighborPivot = neighborPivots[j];
                    var neighborXIndex = index.x + currentNeighborPivot.x;
                    var neighborYIndex = index.y + currentNeighborPivot.y;
                    if (CheckBorders(neighborXIndex, neighborYIndex)) continue;
                    currentGrid.neighbors.Add(grids[neighborXIndex, neighborYIndex]);
                }
            }
        }

        public Vector2Int CalculateGridIndex(int i)
        {
            return new Vector2Int(i % gridN, i / gridN);
        }
        
        
        public bool CheckBorders(int neighborXIndex, int neighborYIndex)
        {
            if (neighborXIndex >= gridN || neighborXIndex < 0 ||
                neighborYIndex >= gridN || neighborYIndex < 0)
                return true;
            
            return false;
        }

        public void GridClick(Grid grid)
        {
            neighborGrids.Clear();
            CheckNeighbors(grid);
            if (neighborGrids.Count > 2)
            {
                count++;
                Debug.Log(count);
                for (int i = 0; i < neighborGrids.Count; i++)
                {
                    neighborGrids[i].isMarked = false;
                    neighborGrids[i].xTextObject.SetActive(false);
                }
                neighborGrids.Clear();
            }
        }

        public void CheckNeighbors(Grid grid)
        {
            if (neighborGrids.Contains(grid)) return;
            neighborGrids.Add(grid);

            for (int i = 0; i < grid.neighbors.Count; i++)
            {
                var currentGrid = grid.neighbors[i];
                if (currentGrid.isMarked)
                {
                    CheckNeighbors(currentGrid);
                }
            }
        }
    }
}