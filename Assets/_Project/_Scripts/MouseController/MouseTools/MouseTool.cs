using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

namespace GardenProject
{
    [System.Serializable]
    public abstract class MouseTool
    {
        public MouseCourserManager.CursorType CursorType { get; protected set; }
        public abstract void UseTool(Coordinate coord);
    }
}
