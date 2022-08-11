using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    [CreateAssetMenu(fileName = "New GameDataSO", menuName = "Data/GameDataSO")]
    public class GameDataSO : ScriptableObject
    {
        public Vector3 refPos;
        public Vector3 refScale;
        public FinishPool finishPool;
        public CubePool cubePool;
        public float tolerance;
    }
}

