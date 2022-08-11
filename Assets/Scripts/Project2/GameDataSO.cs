using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    [CreateAssetMenu(fileName = "New GameDataSO", menuName = "Data/GameDataSO")]
    public class GameDataSO : ScriptableObject
    {
        [HideInInspector] public Vector3 refPos;
        [HideInInspector] public Vector3 refScale;
        [HideInInspector] public FinishPool finishPool;
        [HideInInspector] public CubePool cubePool;
        public float tolerance;
        public List<Color> colors;
    }
}