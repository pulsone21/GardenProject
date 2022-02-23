using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;


namespace GardenProject
{
    public class MouseController : MonoBehaviour
    {
        public static MouseController _instance;
        private Camera m_mainCam;
        [SerializeField] private GameObject PrefabHoverHighlightUI;
        private GameObject hoverUI;

        private MouseTool selectedTool;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
            hoverUI = Instantiate(PrefabHoverHighlightUI, Vector3.zero, Quaternion.identity);
            hoverUI.SetActive(false);
            m_mainCam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                selectedTool = null;
            }

            if (OverGrid(Input.mousePosition, out Coordinate currentCord))
            {
                DisplayHoverUI(currentCord);
                if (selectedTool != null && Input.GetMouseButtonDown(0))
                {
                    selectedTool.UseTool(currentCord);
                }
            }
            else
            {
                hoverUI.SetActive(false);
            }


        }

        private void DisplayHoverUI(Coordinate currentCord)
        {
            hoverUI.transform.position = GridManager._instance.Grid.GetWorldPositionFromGridCoords(currentCord);
            hoverUI.SetActive(true);
        }

        private bool OverGrid(Vector3 _mousePosition, out Coordinate currentCoord)
        {
            GridXZ<GroundTile> grid = GridManager._instance.Grid;
            Vector3 worldPos = grid.GetMouseWorldPosition(_mousePosition);
            currentCoord = grid.GetGridCoordinateFromWorldPos(worldPos);
            if (currentCoord == null) return false;
            return grid.ValidateCoords(currentCoord);
        }

        public void SetMouseTool(MouseTool _mouseTool)
        {
            selectedTool = _mouseTool;
        }
    }
}
