using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class PlayerMoveState : IState
    {
        private PlayerDataSO _playerDataSO;
        private GameDataSO _gameDataSO;
        private Transform _playerTransform;
        private float _refVelocity;

        public PlayerMoveState(PlayerDataSO playerDataSO, GameDataSO gameDataSO)
        {
            _playerDataSO = playerDataSO;
            _gameDataSO = gameDataSO;
        }

        public void Enter()
        {
            _playerTransform = _playerDataSO.playerTransform;
        }

        public void Tick()
        {
            SetPosition();
            SetRotation();
        }

        private void SetPosition()
        {
            float x = Mathf.SmoothDamp(_playerTransform.position.x, _gameDataSO.refPos.x, ref _refVelocity, 0.1f);
            var targetPos = Time.deltaTime * _playerDataSO.speed * _playerTransform.forward;
            targetPos += _playerTransform.position;
            targetPos.x = x;
            _playerTransform.position = targetPos;
        }

        private void SetRotation()
        {
            Quaternion localRotation = _playerDataSO.modelTransform.localRotation;
            Vector3 target = new Vector3(0f, _refVelocity * 10f, 0f);
            Quaternion newR = Quaternion.RotateTowards(localRotation, Quaternion.Euler(target), 
                500f * Time.deltaTime);
            _playerDataSO.modelTransform.localRotation = newR;
        }

        public void Exit()
        {
            
        }
    }
}