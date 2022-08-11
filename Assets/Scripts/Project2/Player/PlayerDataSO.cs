using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    [CreateAssetMenu(fileName = "New PlayerDataSO", menuName = "Data/PlayerDataSO")]
    public class PlayerDataSO : ScriptableObject
    {
        [HideInInspector] public Transform playerTransform;
        [HideInInspector] public Transform modelTransform;
        [HideInInspector] public Player player;
        [HideInInspector] public PlayerSM playerSM;
        public float initialSpeed;
        public float currentSpeed;
    }
}

