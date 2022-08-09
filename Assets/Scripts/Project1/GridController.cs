using System.Collections.Generic;
using UnityEngine;

namespace Project1
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GridDataSO gridData;
        [SerializeField] private IntEvent matchCountEvent;
        [SerializeField] private GridPool gridPool;
        private Grid[,] grids;
        private List<Grid> neighborGrids = new List<Grid>();

        private void Start()
        {
            Create();
        }

        private void Create()
        {
            CreateGrid();
            SetNeighbors();
        }

        private void CreateGrid()
        {
            grids = new Grid[gridData.gridN, gridData.gridN];
            var offset = gridData.gridN / 2f - 0.5f;

            for (var i = 0; i < gridData.GridCount; i++)
            {
                var gridIndex = CalculateGridIndex(i);
                var xPosition = gridIndex.x - offset;
                var yPosition = offset - gridIndex.y;

                var currentGrid = gridPool.ActivateFromPool();
                currentGrid.transform.position = new Vector3(xPosition, yPosition, 0f);
                currentGrid.gameObject.SetActive(true);
                grids[gridIndex.x, gridIndex.y] = currentGrid;
            }
        }

        private void SetNeighbors()
        {
            for (int i = 0; i < gridData.GridCount; i++)
            {
                var index = CalculateGridIndex(i);
                var currentGrid = grids[index.x, index.y];

                for (int j = 0; j < gridData.neighborPivots.Length; j++)
                {
                    var currentNeighborPivot = gridData.neighborPivots[j];
                    var neighborXIndex = index.x + currentNeighborPivot.x;
                    var neighborYIndex = index.y + currentNeighborPivot.y;
                    if (CheckBorders(neighborXIndex, neighborYIndex)) continue;
                    currentGrid.AddNeighbor(grids[neighborXIndex, neighborYIndex]);
                }
            }
        }

        private Vector2Int CalculateGridIndex(int i)
        {
            return new Vector2Int(i % gridData.gridN, i / gridData.gridN);
        }

        private bool CheckBorders(int neighborXIndex, int neighborYIndex)
        {
            if (neighborXIndex >= gridData.gridN || neighborXIndex < 0 ||
                neighborYIndex >= gridData.gridN || neighborYIndex < 0)
                return true;

            return false;
        }

        public void GridClick(Grid grid)
        {
            neighborGrids.Clear();
            CheckNeighbors(grid);
            if (neighborGrids.Count > 2)
            {
                gridData.matchCount++;
                matchCountEvent.Raise(gridData.matchCount);
                for (int i = 0; i < neighborGrids.Count; i++)
                {
                    neighborGrids[i].CloseGrid();
                }

                neighborGrids.Clear();
            }
        }

        private void CheckNeighbors(Grid grid)
        {
            if (neighborGrids.Contains(grid)) return;
            neighborGrids.Add(grid);

            for (int i = 0; i < grid.NeighborCount; i++)
            {
                var currentGrid = grid.GetNeighborAtIndex(i);
                if (currentGrid.IsMarked)
                {
                    CheckNeighbors(currentGrid);
                }
            }
        }

        public void Rebuild(int gridN)
        {
            CleanGrids();
            gridData.SetValues(gridN, 0);
            Create();
        }

        private void CleanGrids()
        {
            for (int i = 0; i < grids.Length; i++)
            {
                var index = CalculateGridIndex(i);
                gridPool.ReturnToPool(grids[index.x, index.y]);
            }
        }

        private void OnDestroy()
        {
            gridData.SetValues(5, 0);
        }
    }
}