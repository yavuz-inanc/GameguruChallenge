using TMPro;
using UnityEngine;

namespace Project1
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI matchCountText;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private IntEvent rebuildEvent;
        
        public void SetMatchCountText(int value)
        {
            matchCountText.SetText($"Match Count = {value}");
        }

        public void RebuildButtonClick()
        {
            rebuildEvent.Raise(int.Parse(inputField.text));
            SetMatchCountText(0);
        }
    }
}
