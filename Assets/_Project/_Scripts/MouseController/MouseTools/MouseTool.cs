using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

namespace GardenProject
{
    [System.Serializable]
    public abstract class MouseTool
    {
        public abstract void UseTool(Coordinate coord);
    }
}
