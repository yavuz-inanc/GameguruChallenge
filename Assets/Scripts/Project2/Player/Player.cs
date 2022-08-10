using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerDataSO playerDataSO;
        [SerializeField] private VoidEvent fallEvent;
        [SerializeField] private BoxCollider collider;

        private void Awake()
        {
            playerDataSO.player = this;
            playerDataSO.playerTransform = transform;
        }

        private void OnTriggerExit(Collider other)
        {
            
            if (Physics.Raycast(transform.position + transform.up, -transform.up, out RaycastHit hit))
            {
                //if (hit.collider.TryGetComponent(out IWalkable walkable)) return;
                //Fall();
            }
            else
            {
                Fall();
            }
        }

        public void Fall()
        {
            collider.enabled = false;
            fallEvent.Raise();
        }
    }
}

