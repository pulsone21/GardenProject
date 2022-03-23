using UnityEngine;
using InventorySystem;
using System;

namespace GardenProject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Fruit")]
    public class Fruit : ScriptableObject, IInventoryObject
    {
        [SerializeField] private int m_baseCost;
        [SerializeField] private new string name;
        [SerializeField] private Sprite uiVisiuals;

        public int Cost => CalculateCost();

        public string Name => name;

        public Sprite UiVisual => uiVisiuals;

        public void PickItem(Inventory inventory)
        {
            Debug.Log("Cannot PickUp a Fruit/Vegetable");
        }

        private int CalculateCost()
        {
            return m_baseCost; //TODO implement some kind of econemie 
        }


    }
}