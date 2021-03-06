using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TimeSystem;
using InventorySystem;
namespace GardenProject
{
    public class Plant
    {
        private readonly PlantSeed m_PlantSeed;
        private GrowthStage m_CurrentGrowthStage;
        private int m_CurrentGrowthStageIndex = 0;
        private readonly GroundTile m_GroundTile;

        public GrowthStage CurrentGrowthStage { get => m_CurrentGrowthStage; }
        public bool IsHarvastable { get => m_CurrentGrowthStage.Harvestable; }
        public bool CanGrow { get => m_CurrentGrowthStageIndex < m_PlantSeed.GrowthStages.Length; }
        private Action m_onGrowthStageChange;
        private long m_nextGrowthStageTime;
        public readonly Vector3 Rotation;

        public Plant(PlantSeed _PlantSeed, GroundTile _myGroundTile)
        {
            m_PlantSeed = _PlantSeed;
            m_CurrentGrowthStage = m_PlantSeed.GrowthStages[m_CurrentGrowthStageIndex];
            m_GroundTile = _myGroundTile;
            Rotation = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
            m_nextGrowthStageTime = TimeManager.Instance.CurrentTimeStamp.InMinutes() + Mathf.FloorToInt(m_CurrentGrowthStage.GrowthTime);
            TimeManager.Instance.RegisterForTimeUpdate(UpdateGrowthStage, TimeManager.SubscriptionType.AfterElapse);
        }

        public void RegisterOnGrowthStageChange(Action _action) => m_onGrowthStageChange += _action;
        public void UnregisterOnGrowthStageChange(Action _action) => m_onGrowthStageChange -= _action;

        public void IncreaseGrowthStage() => ChangeGrowthStage(1);
        public void DecreaseGrowthStage() => ChangeGrowthStage(-1);

        private void ChangeGrowthStage(int changeDirection)
        {
            int changedValue = m_CurrentGrowthStageIndex + (changeDirection / Mathf.Abs(changeDirection));
            if (changedValue >= 0 && changedValue < m_PlantSeed.GrowthStages.Length && m_CurrentGrowthStageIndex != changedValue)
            {
                Debug.Log("Old Growth Index: " + m_CurrentGrowthStageIndex);
                m_CurrentGrowthStage = m_PlantSeed.GrowthStages[changedValue];
                m_CurrentGrowthStageIndex = changedValue;
                Debug.Log("new Growth Index: " + m_CurrentGrowthStageIndex);
                m_nextGrowthStageTime = TimeManager.Instance.CurrentTimeStamp.InMinutes() + Mathf.FloorToInt(m_CurrentGrowthStage.GrowthTime);
                m_onGrowthStageChange?.Invoke();
            }
            else if (changedValue < m_PlantSeed.GrowthStages.Length)
            {
                Debug.Log("max GrowthStage reached");
                TimeManager.Instance.UnregisterForTimeUpdate(UpdateGrowthStage, TimeManager.SubscriptionType.AfterElapse);
            }
        }

        public void ResetGrowth()
        {
            m_CurrentGrowthStage = m_PlantSeed.GrowthStages[0];
            m_CurrentGrowthStageIndex = 0;
            m_onGrowthStageChange?.Invoke();
        }

        /// <summary>
        /// Harvest the Plant 
        /// </summary>
        /// <returns></returns>
        public bool Harvest()
        {
            // IDEA harvest expierence could be change the ammount a bit
            int harvestedAmmount = 0;

            if (m_CurrentGrowthStage.Harvestable)
            {
                harvestedAmmount = m_CurrentGrowthStage.HarvestAmount;
                switch (m_CurrentGrowthStage.HarvestType)
                {
                    case HarvestType.harvest:
                        m_GroundTile.RemovePlant();
                        TimeManager.Instance.UnregisterForTimeUpdate(UpdateGrowthStage, TimeManager.SubscriptionType.AfterElapse);
                        StoreHarvestedItems(harvestedAmmount);
                        return true;
                    case HarvestType.collect:
                        DecreaseGrowthStage();
                        StoreHarvestedItems(harvestedAmmount);
                        return true;
                    default: return false;
                }
            }
            return false;
        }

        private void StoreHarvestedItems(int harvestedAmmount)
        {
            Debug.Log("Storing harvested fruits");
            StorageManager.Instance.Inventory.ReceiveInventoryObject(m_PlantSeed.Fruit, harvestedAmmount);
        }

        private void UpdateGrowthStage(TimeStamp _timeStamp)
        {
            if (m_nextGrowthStageTime <= _timeStamp.InMinutes())
            {
                m_nextGrowthStageTime = long.MinValue;
                IncreaseGrowthStage();
            }
        }
    }
}
