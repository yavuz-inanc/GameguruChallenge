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

        private void Start()
        {
            inputField.SetTextWithoutNotify("5");
        }

        public void SetMatchCountText(int value)
        {
            matchCountText.SetText($"Match Count = {value}");
        }
    }
}
