using System;
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

        public void Create()
        {
            CreateGrid();
            SetNeighbors();
        }

        public void CreateGrid()
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

        public void SetNeighbors()
        {
            for (int i = 0; i < gridData.GridCount; i++)
            {
                var index = CalculateGridIndex(i);
                var currentGrid = grids[index.x, index.y];
                currentGrid.neighbors.Clear();

                for (int j = 0; j < gridData.neighborPivots.Length; j++)
                {
                    var currentNeighborPivot = gridData.neighborPivots[j];
                    var neighborXIndex = index.x + currentNeighborPivot.x;
                    var neighborYIndex = index.y + currentNeighborPivot.y;
                    if (CheckBorders(neighborXIndex, neighborYIndex)) continue;
                    currentGrid.neighbors.Add(grids[neighborXIndex, neighborYIndex]);
                }
            }
        }

        public Vector2Int CalculateGridIndex(int i)
        {
            return new Vector2Int(i % gridData.gridN, i / gridData.gridN);
        }

        public bool CheckBorders(int neighborXIndex, int neighborYIndex)
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

        public void Rebuild(int gridN)
        {
            CleanGrids();
            gridData.gridN = gridN;
            gridData.matchCount = 0;
            Create();
        }

        public void CleanGrids()
        {
            for (int i = 0; i < grids.Length; i++)
            {
                var index = CalculateGridIndex(i);
                gridPool.ReturnToPool(grids[index.x, index.y]);
            }
        }

        private void OnDestroy()
        {
            gridData.gridN = 5;
            gridData.matchCount = 0;
        }
    }
}