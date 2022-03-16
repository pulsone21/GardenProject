using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using TMPro;
using System;

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
        protected TileVisualController baseVisual;
        protected GameObject DebugTextGO;

        public GroundTile(int x, int z, GridXZ<GroundTile> grid)
        {
            m_x = x;
            m_z = z;
            this.grid = grid;
            baseTile = new GameObject(ToString());
            baseTile.transform.position = WorldPosition();
            baseTile.transform.SetParent(grid.GridManagerTransform);
            baseVisual = GameObject.Instantiate(grid.GridManagerTransform.GetComponent<GridManager>().GroundTileVisual, WorldPosition(), Quaternion.identity).GetComponent<TileVisualController>();
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

        public void SetPlantable(bool state)
        {
            if (state) SetPlantable(false);
            baseVisual.VisualChangeOnPlantableChange(state);
            m_IsPlantable = state;
        }
        public void SetPlaceable(bool state)
        {
            if (state) SetPlantable(false);
            m_isPlaceable = state;
        }

        public override string ToString() => $"GardenTile_{m_x},{m_z}";
        public void ToogleDebugText() => DebugTextGO.SetActive(!DebugTextGO.activeSelf);
        public void UpdateDebugText(string newText) => DebugTextGO.GetComponent<TextMeshPro>().text = newText;


        public bool PlantPlant(PlantSeed _plantSeed)
        {
            if (m_IsPlantable)
            {
                m_Plant = new Plant(_plantSeed, this);
                UpdateVisuals();
                m_Plant.RegisterOnGrowthStageChange(UpdateVisuals);
                return true;
            }
            return false;
        }

        private void UpdateVisuals()
        {
            GameObject.Destroy(placedVisual);
            placedVisual = null;
            grid.GetWorldPositionFromGridCoords(m_x, m_z, out Vector3 myWorldPos);
            placedVisual = GameObject.Instantiate(m_Plant.CurrentGrowthStage.Visual, myWorldPos, Quaternion.identity);
            placedVisual.transform.GetChild(0).eulerAngles = m_Plant.Rotation;
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
