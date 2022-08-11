using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class CollectableManager : MonoBehaviour
    {
        [SerializeField] private List<CollectableBase> collectablePrefabs;
        [SerializeField] private GameDataSO gameDataSO;

        private readonly List<int> _zPositionPivots = new List<int>()
        {
            4, 6, 8, 10, 12, 14, 16, 18
        };

        public void CreateCollectables()
        {
            var zPivotsClone = new List<int>(_zPositionPivots);
            int randomCollectableCount = Random.Range(1, 6);
            for (int i = 0; i < randomCollectableCount; i++)
            {
                int randomPivotIndex = Random.Range(0, zPivotsClone.Count);
                int pivot = zPivotsClone[randomPivotIndex];
                zPivotsClone.RemoveAt(randomPivotIndex);
                var collectableObj =
                    Instantiate(collectablePrefabs[Random.Range(0, collectablePrefabs.Count)].gameObject);
                var pos = collectableObj.transform.position;
                pos.z = pivot + gameDataSO.refPos.z;
                collectableObj.transform.position = pos;
            }
        }
    }
}
