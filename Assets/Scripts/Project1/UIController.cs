using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project1
{
    public class UIController : MonoBehaviour
    {
        public TextMeshProUGUI matchCountText;
        public TMP_InputField inputField;
        public VoidEvent rebuildEvent;
        public GridDataSO gridData;
        
        private void Start()
        {
            inputField.SetTextWithoutNotify(gridData.gridN.ToString());
        }

        public void SetMatchCountText(int value)
        {
            matchCountText.SetText($"Match Count = {value}");
        }

        public void RebuildButtonClick()
        {
            gridData.gridN = int.Parse(inputField.text);
            rebuildEvent.Raise();
        }
    }
}
