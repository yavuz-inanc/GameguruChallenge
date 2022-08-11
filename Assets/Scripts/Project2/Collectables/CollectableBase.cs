using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Project2
{
    public abstract class CollectableBase : MonoBehaviour, ICollectable
    {
        [SerializeField] private ParticleSystem particle;
        
        private void Start()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f,45f,0f));
            transform.DORotate(new Vector3(0f, -45f, 0f), 1f).SetLoops(-1, LoopType.Yoyo);
        }

        public virtual void Collect()
        {
            particle.transform.parent = null;
            particle.Play();
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
