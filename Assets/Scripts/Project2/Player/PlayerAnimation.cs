using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project2
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private int _run = Animator.StringToHash("Run");
        private int _dance = Animator.StringToHash("Dance");
        private int _fall = Animator.StringToHash("Fall");

        public void TriggerRun()
        {
            animator.SetTrigger(_run);
        }

        public void TriggerDance()
        {
            animator.SetTrigger(_dance);
        }

        public void TriggerFall()
        {
            animator.SetTrigger(_fall);
        }
    }
}
