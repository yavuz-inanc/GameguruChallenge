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
        public IntEvent rebuildEvent;
        
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
