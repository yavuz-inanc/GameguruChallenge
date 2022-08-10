using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class PlayerIdleState : IState
    {
        private PlayerDataSO _playerDataSO;

        public PlayerIdleState(PlayerDataSO playerDataSO)
        {
            _playerDataSO = playerDataSO;
        }
        
        public void Enter()
        {
            
        }

        public void Tick()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}

