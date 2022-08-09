using UnityEngine;

namespace Project1
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        private const float RefRatio = 1080f / 1920f;

        public void SetOrthographicSize(int gridN)
        {
            var currentRatio = (float)Screen.width / Screen.height;
            var multiplier = currentRatio / RefRatio;
            mainCamera.orthographicSize = gridN / multiplier;
        }
    }
}