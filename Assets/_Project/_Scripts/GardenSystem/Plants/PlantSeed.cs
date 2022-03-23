using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

namespace GardenProject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Plantseed")]
    public class PlantSeed : ScriptableObject, IInventoryObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Sprite seedUiVisual;
        [SerializeField] private int baseCost;
        [SerializeField] private Fruit fruit;
        [SerializeField] private GrowthStage[] m_GrowthStages;

        public Fruit Fruit => fruit;
        public GrowthStage[] GrowthStages => m_GrowthStages;
        Sprite IInventoryObject.UiVisual { get => seedUiVisual; }
        int IInventoryObject.Cost { get => baseCost; }
        string IInventoryObject.Name { get => name; }

        public void PickItem(Inventory inventory)
        {
            MouseController._instance.SetMouseTool(new SeedTool(this, inventory));
            //TODO Implement custom mouse pointer or something that gives feedback
        }
    }
}
