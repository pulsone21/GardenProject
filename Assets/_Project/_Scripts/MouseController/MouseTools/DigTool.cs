using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

namespace GardenProject
{
    public class DigTool : MouseTool
    {
        public override void UseTool(Coordinate coord)
        {
            GridManager._instance.Grid.gridFields[coord.x, coord.y].SetPlantable(true);
        }
    }
}
