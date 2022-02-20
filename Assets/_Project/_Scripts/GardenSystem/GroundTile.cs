using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using TMPro;

namespace GardenProject
{
    public class GroundTile : IGridDebug
    {
        protected bool m_IsPlantable;
        protected bool m_isPlaceable;
        protected GridXZ<GroundTile> grid;
        protected readonly int m_x, m_z;

        protected Plant m_Plant;
        protected GameObject baseTile;
        protected GameObject placedVisual;
        protected GameObject baseVisual;
        protected GameObject DebugTextGO;

        public GroundTile(int x, int z, GridXZ<GroundTile> grid)
        {
            m_x = x;
            m_z = z;
            this.grid = grid;
            baseTile = new GameObject(ToString());
            baseTile.transform.position = WorldPosition();
            baseTile.transform.SetParent(grid.GridManagerTransform);
            baseVisual = GameObject.Instantiate(grid.GridManagerTransform.GetComponent<GridManager>().GroundTileVisuals[0], WorldPosition(), Quaternion.identity);
            baseVisual.transform.SetParent(baseTile.transform);
            CreateDebugText();
        }

        public Vector2 GridPosition => new Vector2(m_x, m_z);
        public Vector3 WorldPosition()
        {
            grid.GetWorldPositionFromGridCoords(m_x, m_z, out Vector3 worldPosition);
            return worldPosition;
        }

        public bool IsPlaceable { get => m_isPlaceable; }
        public bool IsPlantable { get => m_IsPlantable; }
        public GridXZ<GroundTile> Grid { get => grid; }

        public void TooglePlantable() => m_IsPlantable = !m_IsPlantable;
        public void TooglePlaceable() => m_isPlaceable = !m_isPlaceable;

        public override string ToString() => $"GardenTile_{m_x},{m_z}";
        public void ToogleDebugText() => DebugTextGO.SetActive(!DebugTextGO.activeSelf);
        public void UpdateDebugText(string newText) => DebugTextGO.GetComponent<TextMeshPro>().text = newText;


        public bool PlantPlant(Plant _plant)
        {
            if (m_IsPlantable)
            {
                m_Plant = _plant;
                //TODO Handle inventory Stuff
                //TODO Handle Visuals
                m_Plant.RegisterOnGrowthStageChange(UpdateVisuals);
                return true;
            }
            return false;
        }

        private void UpdateVisuals(GrowthStage growthStage)
        {
            GameObject.Destroy(placedVisual);
            placedVisual = null;
            grid.GetWorldPositionFromGridCoords(m_x, m_z, out Vector3 myWorldPos);
            placedVisual = GameObject.Instantiate(m_Plant.PlantVisuals[(int)growthStage], myWorldPos, Quaternion.identity);
        }

        public bool HarvestPlant()
        {
            bool harvested = m_Plant.Harvest(out int harvestedAmmount);
            //TODO Handle Inventory Stuff
            return harvested;
        }

        public void RemovePlant()
        {
            m_Plant.UnregisterOnGrowthStageChange(UpdateVisuals);
            m_Plant = null;
            GameObject.Destroy(placedVisual);
            placedVisual = null;
        }

        public bool RemovePlaceableObject()
        {
            throw new System.NotImplementedException();
        }

        public bool PlacePlaceabloeObject()
        {
            throw new System.NotImplementedException();
        }

        public void CreateDebugText()
        {
            DebugTextGO = new GameObject($"DebugText_{m_x},{m_z}", typeof(TextMeshPro));
            DebugTextGO.transform.SetParent(baseTile.transform);
            DebugTextGO.GetComponent<RectTransform>().localPosition = new Vector3(grid.CellSize * .5f, 0.1f, grid.CellSize * .5f);
            DebugTextGO.GetComponent<RectTransform>().rotation = Quaternion.Euler(90, 0, 0);
            DebugTextGO.GetComponent<RectTransform>().sizeDelta = new Vector2(grid.CellSize, grid.CellSize);
            TextMeshPro DebugText = DebugTextGO.GetComponent<TextMeshPro>();
            DebugText.alignment = TextAlignmentOptions.Center;
            DebugText.text = ToString();
            DebugText.enableAutoSizing = true;
            DebugText.fontSizeMax = 20;
            DebugText.fontSizeMin = 5;
            DebugText.color = Color.magenta;
            DebugText.margin = new Vector4(0.3f, 0, 0.3f, 0);
        }



    }
}
