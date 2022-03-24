using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
namespace GardenProject
{
    public class FertilizeTool : MouseTool
    {
        public FertilizeTool()
        {
            CursorType = MouseCourserManager.CursorType.Fertilize;
        }

        public override void UseTool(Coordinate coord)
        {
            throw new System.NotImplementedException();
        }
    }
}
