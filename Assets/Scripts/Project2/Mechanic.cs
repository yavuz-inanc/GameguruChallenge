using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Project2
{
    public class Mechanic : MonoBehaviour
    {
        public Transform cube1;
        public Vector3 refPos;
        public Vector3 refScale;
        public Transform currentCube;
        public Transform cuttedCube;
        public float posDiff;
        
        private void Start()
        {
            SetRefValues(cube1.position, cube1.localScale);
            CreateNewCube();
        }

        public void SetRefValues(Vector3 refPos, Vector3 refScale)
        {
            this.refPos = refPos;
            this.refScale = refScale;
        }
        
        public void CreateNewCube()
        {
            var newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            newCube.transform.position = new Vector3(2f, 0f, refPos.z + 1f);
            newCube.transform.localScale = refScale;
            newCube.transform.DOMoveX(-2f, 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

            currentCube = newCube.transform;
        }
        
        [Button()]
        public void TapAction()
        {
            posDiff = currentCube.position.x - refPos.x;
            
            SetCurrentCubeScale();
            SetCurrentCubePos();
            
            cuttedCube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;

            SetCuttedCubeScale();
            SetCuttedCubePos();

            SetRefValues(currentCube.position, currentCube.localScale);
            CreateNewCube();
        }

        public void SetCurrentCubeScale()
        {
            var cubeScale = currentCube.localScale;
            cubeScale.x -= Mathf.Abs(posDiff);
            currentCube.localScale = cubeScale;
        }

        public void SetCurrentCubePos()
        {
            currentCube.DOKill();
            
            var cubePosition = currentCube.position;
            cubePosition.x -= posDiff / 2f;
            currentCube.position = cubePosition;
        }

        public void SetCuttedCubeScale()
        {
            var cuttedCubeScale = cuttedCube.localScale;
            cuttedCubeScale.x = Mathf.Abs(posDiff);
            cuttedCube.localScale = cuttedCubeScale;
        }

        public void SetCuttedCubePos()
        {
            var cuttedCubePosition = currentCube.position;
            cuttedCubePosition.x = Mathf.Sign(posDiff) * refScale.x / 2f + currentCube.position.x;
            cuttedCube.position = cuttedCubePosition;

            cuttedCube.DOMoveY(-5f, 1f).SetEase(Ease.InCubic);
        }
    }
}

