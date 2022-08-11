using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Project2
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip perfectSound;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float pitch = 1f;
        
        public void PerfectMatchAction()
        {
            pitch += 0.25f;
            PlaySound();
        }

        public void ImperfectMatchAction()
        {
            ResetPitch();
        }

        public void ResetPitch()
        {
            pitch = 1f;
        }

        [Button()]
        private void PlaySound()
        {
            audioSource.pitch = pitch;
            audioSource.Pause();
            audioSource.PlayOneShot(perfectSound);
        }
    }
}

