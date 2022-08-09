using System.Collections.Generic;
using UnityEngine;

namespace Project1
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private GameObject xTextObject;
        [SerializeField] private GridEvent gridClickEvent;
        private List<Grid> _neighbors = new List<Grid>();
        
        public bool IsMarked { get; private set; }
        public int NeighborCount => _neighbors.Count;
        
        private void OnMouseDown()
        {
            if (IsMarked) return;
            OpenGrid();
            gridClickEvent.Raise(this);
        }

        public void AddNeighbor(Grid neighbor)
        {
            _neighbors.Add(neighbor);
        }

        public Grid GetNeighborAtIndex(int index)
        {
            return _neighbors[index];
        }

        private void OpenGrid()
        {
            IsMarked = true;
            xTextObject.SetActive(true);
        }
        
        public void CloseGrid()
        {
            IsMarked = false;
            xTextObject.SetActive(false);
        }
        
        public void ResetGrid()
        {
            CloseGrid();
            _neighbors.Clear();
        }
    }
}