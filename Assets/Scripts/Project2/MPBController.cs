using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Project2
{
    public class MPBController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer renderer;

        private MaterialPropertyBlock _mpb;

        public void SetColor(Color color)
        {
            _mpb = new MaterialPropertyBlock();
            _mpb.SetColor("_Color", color);
            renderer.SetPropertyBlock(_mpb);
        }
    }
}

