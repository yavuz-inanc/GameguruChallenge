using System;
using UnityEngine;

namespace Project2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerDataSO playerDataSO;
        [SerializeField] private GameDataSO gameDataSO;
        [SerializeField] private VoidEvent fallEvent;
        [SerializeField] private VoidEvent finishEvent;
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private Transform model;

        private void Awake()
        {
            playerDataSO.player = this;
            playerDataSO.playerTransform = transform;
            playerDataSO.modelTransform = model;
            playerDataSO.currentSpeed = playerDataSO.initialSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Finish finish))
            {
                SetRefValues(other.transform);
                finishEvent.Raise();
            }

            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }

        private void SetRefValues(Transform other)
        {
            gameDataSO.refPos = new Vector3(0f, 0f, other.position.z);
            gameDataSO.refScale = new Vector3(2f, 1f, 2f);
        }

        private void OnTriggerExit(Collider other)
        {
            if (Physics.BoxCast(transform.position + transform.up, boxCollider.bounds.size * 0.25f, 
                    -transform.up, out RaycastHit hit,Quaternion.identity,1f))
            {
                if (hit.collider.TryGetComponent(out IWalkable walkable)) return;
                Fall();
            }
            else
            {
                Fall();
            }
        }

        public void Fall()
        {
            boxCollider.enabled = false;
            fallEvent.Raise();
        }

        public void IncreaseSpeed()
        {
            playerDataSO.currentSpeed *= 3f;
        }

        public void DecreaseSpeed()
        {
            playerDataSO.currentSpeed = playerDataSO.initialSpeed;
        }
    }
}

