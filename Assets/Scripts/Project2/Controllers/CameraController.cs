using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Project2
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera playerCam;
        [SerializeField] private CinemachineVirtualCamera finishCam;
        
        private CinemachineOrbitalTransposer _finishCamOrbitalTransposer;
        private Coroutine _camRotateCoroutine;

        private void Awake()
        {
            _finishCamOrbitalTransposer = finishCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        public void ActivateFinishCam()
        {
            finishCam.enabled = true;
            _camRotateCoroutine = StartCoroutine(CamRotateIE());
        }

        public void DeactivateFinishCam()
        {
            finishCam.enabled = false;
            if (_camRotateCoroutine != null)
            {
                StopCoroutine(_camRotateCoroutine);
            }
        }

        IEnumerator CamRotateIE()
        {
            _finishCamOrbitalTransposer.m_XAxis.Value = 0f;
            while (true)
            {
                _finishCamOrbitalTransposer.m_XAxis.Value +=
                    Time.deltaTime * _finishCamOrbitalTransposer.m_XAxis.m_MaxSpeed;
                yield return null;
            }
        }
    }
}

