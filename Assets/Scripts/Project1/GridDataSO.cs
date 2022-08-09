using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project1
{
    [CreateAssetMenu(fileName = "New GridDataSO", menuName = "Data/GridDataSO")]
    public class GridDataSO : ScriptableObject
    {
        public Grid gridPrefab;
        public int gridN;
        public int matchCount;
        public readonly Vector2Int[] neighborPivots =
        {
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1)
        };
        public int GridCount => gridN * gridN;
    }
}

