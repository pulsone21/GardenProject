using UnityEngine;
using System.Collections.Generic;
using System;
using GardenProject;

namespace GridSystem
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager _instance;
        [SerializeField] private int width, height, cellSize;
        public GridXZ<GroundTile> Grid { get; protected set; }
        public GameObject[] GroundTileVisuals;


        void Awake()
        {
            if (_instance)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }

            GroundTileVisuals = Resources.LoadAll<GameObject>("/GardenTiles");
        }

        private void Start()
        {
            if (Grid == null)
            {
                GenerateGrid();
            }
        }

        internal void GenerateGrid() => Grid = new GridXZ<GroundTile>(width, height, cellSize, this.transform.position, this.transform, (GridXZ<GroundTile> g, int x, int z) => new GroundTile(x, z, g));

        internal void ClearGrid() => Grid = null;



    }
}