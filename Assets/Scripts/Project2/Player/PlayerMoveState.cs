using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class PlayerMoveState : IState
    {
        private PlayerDataSO _playerDataSO;

        public PlayerMoveState(PlayerDataSO playerDataSO)
        {
            _playerDataSO = playerDataSO;
        }
        
        public void Enter()
        {
            
        }

        public void Tick()
        {
            _playerDataSO.playerTransform.position +=
                Time.deltaTime * _playerDataSO.speed * _playerDataSO.playerTransform.forward;
        }

        public void Exit()
        {
            
        }
    }
}

