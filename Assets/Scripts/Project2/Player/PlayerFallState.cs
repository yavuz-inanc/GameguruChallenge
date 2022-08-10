using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Project2
{
    public class PlayerFallState : IState
    {
        private PlayerDataSO _playerDataSO;
        private Transform _playerTransform;
        private float _maxFallSpeed = 5f;
        private float _fallSpeed;
        
        public PlayerFallState(PlayerDataSO playerDataSO)
        {
            _playerDataSO = playerDataSO;
        }
        
        public void Enter()
        {
            _playerTransform = _playerDataSO.playerTransform;
            _playerDataSO.player.StartCoroutine(IncreaseFallSpeed());
        }

        public void Tick()
        {
            _playerTransform.position += Time.deltaTime * _fallSpeed * Vector3.down;
        }

        public void Exit()
        {
            
        }

        IEnumerator IncreaseFallSpeed()
        {
            var time = 0f;
            while (true)
            {
                if (time >= 1f)
                {
                    _fallSpeed = _maxFallSpeed;
                    yield break;
                }

                _fallSpeed = Mathf.Lerp(0f, _maxFallSpeed, time);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}

