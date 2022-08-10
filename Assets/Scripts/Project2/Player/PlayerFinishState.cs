using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Project2
{
    public class PlayerFinishState : IState
    {
        private PlayerDataSO _playerDataSO;
        private GameDataSO _gameDataSO;

        public PlayerFinishState(PlayerDataSO playerDataSO, GameDataSO gameDataSO)
        {
            _playerDataSO = playerDataSO;
            _gameDataSO = gameDataSO;
        }
        
        public void Enter()
        {
            var playerTransform = _playerDataSO.playerTransform;
            playerTransform.DOMove(new Vector3(_gameDataSO.refPos.x, playerTransform.position.y, _gameDataSO.refPos.z),
                0.5f);
        }

        public void Tick()
        {
            
        }

        public void Exit()
        {
            
        }
    }

}
