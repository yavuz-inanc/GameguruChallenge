using System.Collections.Generic;
using UnityEngine;

namespace Project1
{
    public class Grid : MonoBehaviour
    {
        public List<Vector2Int> neighborIndexes = new List<Vector2Int>();
        public Vector2Int index;

        public void SetNeighbors(Vector2Int index, int grid_N)
        {
            this.index = index;
            for (int i = 0; i < GridController.NeighborPivots.Length; i++)
            {
                var currentNeighborPivot = GridController.NeighborPivots[i];
                if (CheckBorders(currentNeighborPivot, grid_N)) continue;
                neighborIndexes.Add(CalculateNeighborIndex(currentNeighborPivot));
            }
        }

        public bool CheckBorders(Vector2Int neighborPivot, int grid_N)
        {
            if (neighborPivot.x + index.x >= grid_N || neighborPivot.x + index.x < 0 ||
                neighborPivot.y + index.y >= grid_N || neighborPivot.y + index.y < 0)
                return true;
            
            return false;
        }

        public Vector2Int CalculateNeighborIndex(Vector2Int neighborPivot)
        {
            return new Vector2Int(neighborPivot.x + index.x, neighborPivot.y + index.y);
        }
    }
}