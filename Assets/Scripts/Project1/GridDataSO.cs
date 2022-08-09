using UnityEngine;

namespace Project1
{
    [CreateAssetMenu(fileName = "New GridDataSO", menuName = "Data/GridDataSO")]
    public class GridDataSO : ScriptableObject
    {
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

        public void SetValues(int gridN, int matchCount)
        {
            this.gridN = gridN;
            this.matchCount = matchCount;
        }
    }
}

