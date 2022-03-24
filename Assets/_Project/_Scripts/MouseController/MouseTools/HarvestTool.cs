using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace GardenProject
{
    public class HarvestTool : MouseTool
    {
        public HarvestTool()
        {
            CursorType = MouseCourserManager.CursorType.Harvest;
        }

        public override void UseTool(Coordinate coord)
        {
            GridManager._instance.Grid.gridFields[coord.x, coord.y].HarvestPlant();
            MouseCourserManager.Instance.SetCursor(MouseCourserManager.CursorType.Harvest);
        }
    }
}
