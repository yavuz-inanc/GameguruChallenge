using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class CubePool : ObjectPoolMB<Cube>
    {
        [SerializeField] private GameDataSO gameDataSO;
        protected override void Awake()
        {
            base.Awake();
            gameDataSO.cubePool = this;
        }
    }
}

