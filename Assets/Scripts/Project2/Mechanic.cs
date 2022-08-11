using DG.Tweening;
using UnityEngine;

namespace Project2
{
    public class Mechanic : MonoBehaviour
    {
        [SerializeField] private GameDataSO gameDataSO;
        [SerializeField] private Transform cube1;
        [SerializeField] private VoidEvent perfectMatch;
        [SerializeField] private VoidEvent imperfectMatch;

        private Transform currentCube;
        private float posDiff;
        private bool isActive;
        private const int MaxPlaceableCubeCount = 8;
        private int _currentPlacedCubeCount;

        private void Start()
        {
            SetRefValues(cube1.position, cube1.localScale);
        }

        public void PlayAction()
        {
            var cube = gameDataSO.cubePool.ActivateFromPool();
            cube.gameObject.SetActive(true);
            cube.position = new Vector3(0f, 0f, gameDataSO.refPos.z + gameDataSO.refScale.z);
            cube.localScale = gameDataSO.refScale;

            var finish = gameDataSO.finishPool.ActivateFromPool();
            finish.gameObject.SetActive(true);
            finish.position = new Vector3(0f, 0.5f, gameDataSO.refPos.z + 20f);

            SetRefValues(cube.position, cube.localScale);
            CreateNewCube();
            isActive = true;
            _currentPlacedCubeCount = 0;
        }

        public void SetRefValues(Vector3 refPos, Vector3 refScale)
        {
            gameDataSO.refPos = refPos;
            gameDataSO.refScale = refScale;
        }

        public void CreateNewCube()
        {
            var newCube = gameDataSO.cubePool.ActivateFromPool();
            newCube.gameObject.SetActive(true);

            newCube.transform.position = new Vector3(2f, 0f, gameDataSO.refPos.z + gameDataSO.refScale.z);
            newCube.transform.localScale = gameDataSO.refScale;
            newCube.transform.DOMoveX(-2f, 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

            currentCube = newCube.transform;
        }

        public void TapAction()
        {
            if (!isActive) return;
            posDiff = currentCube.position.x - gameDataSO.refPos.x;
            if (!PositionDiffCheck()) return;
            if (Mathf.Abs(posDiff) < gameDataSO.tolerance)
            {
                posDiff = 0f;
                perfectMatch.Raise();
            }
            else
            {
                imperfectMatch.Raise();
            }
            
            SetCurrentCubeScale();
            SetCurrentCubePos();

            if (posDiff > 0f)
            {
                var cuttedCube = gameDataSO.cubePool.ActivateFromPool();
                cuttedCube.gameObject.SetActive(true);

                SetCuttedCubeScale(cuttedCube);
                SetCuttedCubePos(cuttedCube);
            }

            SetRefValues(currentCube.position, currentCube.localScale);

            if (CheckPlacedCubeCount()) return;

            CreateNewCube();
        }

        public bool PositionDiffCheck()
        {
            if (Mathf.Abs(posDiff) >= currentCube.localScale.x)
            {
                currentCube.DOKill();
                currentCube.DOMoveY(-10f, 1f).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    gameDataSO.cubePool.ReturnToPool(currentCube);
                });
                isActive = false;
                return false;
            }

            return true;
        }

        private bool CheckPlacedCubeCount()
        {
            _currentPlacedCubeCount++;
            if (_currentPlacedCubeCount == MaxPlaceableCubeCount)
            {
                isActive = false;
                return true;
            }

            return false;
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
            if (posDiff > 0f)
            {
                cubePosition.x -= posDiff / 2f;
            }
            else
            {
                cubePosition.x = gameDataSO.refPos.x;
            }
            currentCube.position = cubePosition;
        }

        public void SetCuttedCubeScale(Transform cuttedCube)
        {
            var cuttedCubeScale = currentCube.localScale;
            cuttedCubeScale.x = Mathf.Abs(posDiff);
            cuttedCube.localScale = cuttedCubeScale;
        }

        public void SetCuttedCubePos(Transform cuttedCube)
        {
            var cuttedCubePosition = currentCube.position;
            cuttedCubePosition.x = Mathf.Sign(posDiff) * gameDataSO.refScale.x / 2f + currentCube.position.x;
            cuttedCube.position = cuttedCubePosition;

            cuttedCube.DOMoveY(-10f, 1f).SetEase(Ease.InCubic).OnComplete(() =>
            {
                gameDataSO.cubePool.ReturnToPool(cuttedCube);
            });
        }
    }
}