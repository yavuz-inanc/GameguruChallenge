using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private VoidEvent playEvent;
        
        public void PlayButtonClick()
        {
            playEvent.Raise();
        }
    }
}

