using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GardenProject
{
    public class ToolManager : MonoBehaviour
    {
        [SerializeField] private MouseController mouseController;

        public void SelectDigTool()
        {
            DigTool digTool = new DigTool();
            mouseController.SetMouseTool(digTool);
        }

        public void SelectFetilizeTool()
        {
            FertilizeTool fertilizedTool = new FertilizeTool();
            mouseController.SetMouseTool(fertilizedTool);
        }

        public void SelectHarvestTool()
        {
            HarvestTool harvestTool = new HarvestTool();
            mouseController.SetMouseTool(harvestTool);
        }

        public void SelectDestroyTool()
        {
            DestoryTool destoryTool = new DestoryTool();
            mouseController.SetMouseTool(destoryTool);
        }
    }
}
