using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using InventorySystem;
namespace GardenProject
{
    [System.Serializable]
    public class SeedTool : MouseTool
    {
        public readonly Plant PlantSeed;
        public Inventory Inventory { get; protected set; }


        public SeedTool(Plant plantSeed, Inventory inventory)
        {
            PlantSeed = plantSeed;
            Inventory = inventory;
        }

        public bool HasSeeds => Inventory.GetIventoryObjectAmount(PlantSeed) > 0;


        public override void UseTool(Coordinate coord)
        {
            GroundTile groundTile = GridManager._instance.Grid.gridFields[coord.x, coord.y];
            if (groundTile.IsPlantable && Inventory.IssueInventoryObject(PlantSeed, 1))
            {
                groundTile.PlantPlant(PlantSeed);
            }
            else
            {
                Debug.Log("GroundTile is not planatable or we dont have Seeds");
            }

            //TODO Find a way to automaticly deslect the seed tool if no seeds are left
        }


    }
}
