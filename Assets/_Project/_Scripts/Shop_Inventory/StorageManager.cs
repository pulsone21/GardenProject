using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardenProject;

namespace InventorySystem
{
    public class StorageManager : InventoryManager
    {
        public static StorageManager Instance;

        public bool TESTING_InventoryPlaces;

        public List<PlantSeed> TESTING_Plants = new List<PlantSeed>();

        protected void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }


            //TESTING!!!
            if (TESTING_InventoryPlaces)
            {
                List<InventoryPlace> ips = new List<InventoryPlace>();
                foreach (PlantSeed plant in TESTING_Plants)
                {
                    ips.Add(new InventoryPlace(plant, Random.Range(1, 15)));
                }
                Inventory = new Inventory(ips);
            }
            else
            {
                Inventory = new Inventory();
            }
            Inventory.RegisterForOnIssue(UpdateUI);
            Inventory.RegisterForOnReceive(UpdateUI);
        }

        private void Start()
        {

            InitItemCards();
        }

        private void InitItemCards()
        {
            InventoryPlace[] IPs = Inventory.GetAllObjects();
            foreach (InventoryPlace ip in IPs)
            {
                if (ip.CurrentAmount < 1) continue;
                GameObject go = Instantiate(m_itemCard, Vector3.zero, Quaternion.identity);
                go.GetComponent<ItemCardController>().InitializeItemCard(ip.Object, this, ItemCardController.Context.Pick);
                go.transform.SetParent(transform);
            }
        }

        private void UpdateUI()
        {
            transform.ClearAllChildren();
            InitItemCards();
        }






    }
}
