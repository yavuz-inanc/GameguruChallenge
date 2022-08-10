using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class FinishPool : ObjectPoolMB<Transform>
    {
        [SerializeField] private GameDataSO gameDataSO;
        protected override void Awake()
        {
            base.Awake();
            gameDataSO.finishPool = this;
        }
    }
}

