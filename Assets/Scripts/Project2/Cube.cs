using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class Cube : MonoBehaviour, IWalkable
    {
        [SerializeField] private MPBController mpb;

        public void SetColor(Color color)
        {
            mpb.SetColor(color);
        }
    }
}

