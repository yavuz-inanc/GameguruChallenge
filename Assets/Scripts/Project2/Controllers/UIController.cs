using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project2
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private VoidEvent playEvent;
        [SerializeField] private VoidEvent tryAgainEvent;
        [SerializeField] private Button playButton;
        [SerializeField] private Button tryAgainButton;
        
        public void PlayButtonClick()
        {
            playButton.gameObject.SetActive(false);
            playEvent.Raise();
        }
        
        public void TryAgainButtonClick()
        {
            tryAgainEvent.Raise();
        }

        public void FinishAction()
        {
            Invoke(nameof(ActivatePlayButton), 1f);
        }

        public void FallAction()
        {
            Invoke(nameof(ActivateTryAgainButton), 1.5f);
        }

        private void ActivatePlayButton()
        {
            playButton.gameObject.SetActive(true);
        }
        
        private void ActivateTryAgainButton()
        {
            tryAgainButton.gameObject.SetActive(true);
        }
    }
}

