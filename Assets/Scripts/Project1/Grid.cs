using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project1
{
    public class Grid : MonoBehaviour
    {
        public List<Grid> neighbors = new List<Grid>();
        public bool isMarked;
        public GameObject xTextObject;
        public GridEvent gridClickEvent;

        private void OnMouseDown()
        {
            if (isMarked) return;
            isMarked = true;
            xTextObject.SetActive(true);
            gridClickEvent.Raise(this);
        }
    }
}