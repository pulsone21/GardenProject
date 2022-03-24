using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

namespace GardenProject
{
    public class DigTool : MouseTool
    {
        public DigTool()
        {
            CursorType = MouseCourserManager.CursorType.Dig;
        }

        public override void UseTool(Coordinate coord)
        {
            GridManager._instance.Grid.gridFields[coord.x, coord.y].SetPlantable(true);
            MouseCourserManager.Instance.SetCursor(MouseCourserManager.CursorType.Dig);
        }
    }
}
