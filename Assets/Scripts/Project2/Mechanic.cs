using DG.Tweening;
using UnityEngine;

namespace Project2
{
    public class Mechanic : MonoBehaviour
    {
        [SerializeField] private GameDataSO gameDataSO;
        [SerializeField] private Cube cube1;
        [SerializeField] private VoidEvent perfectMatch;
        [SerializeField] private VoidEvent imperfectMatch;
        [SerializeField] private VoidEvent cubesPlaced;

        private Cube currentCube;
        private float posDiff;
        private bool isActive;
        private const int MaxPlaceableCubeCount = 8;
        private int _currentPlacedCubeCount;
        private int colorCounter;
        
        private float PosDiffAbs => Mathf.Abs(posDiff);

        
        private void Start()
        {
            SetRefValues(cube1.transform.position, cube1.transform.localScale);
            SetCubeColor(cube1, true);
        }

        public void PlayAction()
        {
            var cube = gameDataSO.cubePool.ActivateFromPool();
            cube.gameObject.SetActive(true);
            cube.transform.position = new Vector3(0f, 0f, gameDataSO.refPos.z + gameDataSO.refScale.z);
            cube.transform.localScale = gameDataSO.refScale;
            SetCubeColor(cube, true);

            var finish = gameDataSO.finishPool.ActivateFromPool();
            finish.gameObject.SetActive(true);
            finish.position = new Vector3(0f, 0.5f, gameDataSO.refPos.z + 20f);

            SetRefValues(cube.transform.position, cube.transform.localScale);
            CreateNewCube();
            isActive = true;
            _currentPlacedCubeCount = 0;
        }

        private void SetCubeColor(Cube cube, bool increaseCounter = false)
        {
            cube.SetColor(gameDataSO.colors[colorCounter % gameDataSO.colors.Count]);
            colorCounter = increaseCounter ? colorCounter + 1 : colorCounter;
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

            var sign = _currentPlacedCubeCount % 2 == 0 ? 1 : -1;
            newCube.transform.position = new Vector3(sign * 2f, 0f, gameDataSO.refPos.z + gameDataSO.refScale.z);
            newCube.transform.localScale = gameDataSO.refScale;
            newCube.transform.DOMoveX(sign * -2f, 1.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

            currentCube = newCube;
            SetCubeColor(newCube);
        }

        public void TapAction()
        {
            if (!isActive) return;
            posDiff = currentCube.transform.position.x - gameDataSO.refPos.x;

            if (!PositionDiffCheck()) return;
            if (PosDiffAbs < gameDataSO.tolerance)
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

            if (PosDiffAbs > 0f)
            {
                var cuttedCube = gameDataSO.cubePool.ActivateFromPool();
                cuttedCube.gameObject.SetActive(true);
                SetCuttedCubeScale(cuttedCube);
                SetCuttedCubePos(cuttedCube);
                SetCubeColor(cuttedCube);
            }

            colorCounter++;

            SetRefValues(currentCube.transform.position, currentCube.transform.localScale);

            if (CheckPlacedCubeCount()) return;

            CreateNewCube();
        }

        public bool PositionDiffCheck()
        {
            if (PosDiffAbs >= currentCube.transform.localScale.x)
            {
                currentCube.transform.DOKill();
                currentCube.transform.DOMoveY(-10f, 1f).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    gameDataSO.cubePool.ReturnToPool(currentCube);
                });
                isActive = false;
                cubesPlaced.Raise();
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
                cubesPlaced.Raise();
                return true;
            }

            return false;
        }

        public void SetCurrentCubeScale()
        {
            var cubeScale = currentCube.transform.localScale;
            cubeScale.x -= PosDiffAbs;
            currentCube.transform.localScale = cubeScale;
        }

        public void SetCurrentCubePos()
        {
            currentCube.transform.DOKill();

            var cubePosition = currentCube.transform.position;
            if (PosDiffAbs > 0f)
            {
                cubePosition.x -= posDiff / 2f;
            }
            else
            {
                cubePosition.x = gameDataSO.refPos.x;
            }
            currentCube.transform.position = cubePosition;
        }

        public void SetCuttedCubeScale(Cube cuttedCube)
        {
            var cuttedCubeScale = currentCube.transform.localScale;
            cuttedCubeScale.x = PosDiffAbs;
            cuttedCube.transform.localScale = cuttedCubeScale;
        }

        public void SetCuttedCubePos(Cube cuttedCube)
        {
            var cuttedCubePosition = currentCube.transform.position;
            cuttedCubePosition.x = Mathf.Sign(posDiff) * gameDataSO.refScale.x / 2f + currentCube.transform.position.x;
            cuttedCube.transform.position = cuttedCubePosition;

            cuttedCube.transform.DOMoveY(-10f, 1f).SetEase(Ease.InCubic).OnComplete(() =>
            {
                gameDataSO.cubePool.ReturnToPool(cuttedCube);
            });
        }
    }
}