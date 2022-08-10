using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class PlayerSM : StateMachineMB
    {
        [SerializeField] private PlayerDataSO playerDataSO;
        [SerializeField] private GameDataSO gameDataSO;
        
        private IState _playerIdleState;
        private IState _playerMoveState;
        private IState _playerFallState;
        private IState _playerFinishState;

        
        private void Awake()
        {
            playerDataSO.playerSM = this;
            
            _playerIdleState = new PlayerIdleState(playerDataSO);
            _playerMoveState = new PlayerMoveState(playerDataSO);
            _playerFallState = new PlayerFallState(playerDataSO);
            _playerFinishState = new PlayerFinishState(playerDataSO, gameDataSO);
            
            ChangeStateToIdle();
        }

        public void ChangeStateToIdle()
        {
            ChangeState(_playerIdleState);
        }

        public void ChangeStateToMove()
        {
            ChangeState(_playerMoveState);
        }

        public void ChangeStateToFall()
        {
            ChangeState(_playerFallState);
        }

        public void ChangeStateToFinish()
        {
            ChangeState(_playerFinishState);
        }
    }
}

