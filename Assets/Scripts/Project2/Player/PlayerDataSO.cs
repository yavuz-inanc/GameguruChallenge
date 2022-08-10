using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    [CreateAssetMenu(fileName = "New PlayerDataSO", menuName = "Data/PlayerDataSO")]
    public class PlayerDataSO : ScriptableObject
    {
        public Transform playerTransform;
        public Player player;
        public PlayerSM playerSM;
        public float speed;
    }
}

