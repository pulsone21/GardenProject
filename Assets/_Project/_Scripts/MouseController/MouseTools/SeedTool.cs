using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

namespace GardenProject
{
    [System.Serializable]
    public class SeedTool : MouseTool
    {
        public readonly Plant PlantSeed;
        public int SeedAmount { get; protected set; }

        public SeedTool(Plant plantSeed, int seedAmount)
        {
            PlantSeed = plantSeed;
            SeedAmount = seedAmount;
        }

        public bool HasSeeds => SeedAmount > 0;

        public override void UseTool(Coordinate coord)
        {
            throw new System.NotImplementedException();
        }


    }
}
