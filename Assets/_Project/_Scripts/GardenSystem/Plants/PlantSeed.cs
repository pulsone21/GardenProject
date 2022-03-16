using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

namespace GardenProject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Plantseed")]
    public class PlantSeed : ScriptableObject, IInventoryObject
    {
        public string Name;
        [SerializeField] private GrowthStage[] m_GrowthStages;
        public Sprite UiVisual;
        public int BaseCost;
        public GrowthStage[] GrowthStages => m_GrowthStages;

        Sprite IInventoryObject.UiVisual { get => UiVisual; }
        int IInventoryObject.BaseCost { get => BaseCost; }
        string IInventoryObject.Name { get => Name; }

        public void PickItem(Inventory inventory)
        {
            MouseController._instance.SetMouseTool(new SeedTool(this, inventory));
            //TODO Implement custom mouse pointer or something that gives feedback
        }
    }
}
