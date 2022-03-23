using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace GardenProject
{
    public class HarvestTool : MouseTool
    {
        public override void UseTool(Coordinate coord)
        {
            GridManager._instance.Grid.gridFields[coord.x, coord.y].HarvestPlant();
        }
    }
}
