using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project2
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private VoidEvent playEvent;
        [SerializeField] private Button playButton;
        
        public void PlayButtonClick()
        {
            playButton.gameObject.SetActive(false);
            playEvent.Raise();
        }

        public void FinishAction()
        {
            Invoke(nameof(ActivatePlayButton), 0.5f);
        }

        private void ActivatePlayButton()
        {
            playButton.gameObject.SetActive(true);
        }
    }
}

