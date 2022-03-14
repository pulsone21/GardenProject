using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace GardenProject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Plant")]
    public class Plant : ScriptableObject
    {
        public string Name;
        [SerializeField] private GrowthStage[] m_GrowthStages;
        public Sprite UiVisual;

        private GrowthStage m_CurrentGrowthStage;
        private int m_CurrentGrowthStageIndex = 0;
        private GroundTile m_MyGroundTile;

        public GrowthStage CurrentGrowthStage { get => m_CurrentGrowthStage; }
        public bool IsHarvastable { get => m_CurrentGrowthStage.Harvestable; }

        private Action m_onGrowthStageChange;

        public void RegisterOnGrowthStageChange(Action _action) => m_onGrowthStageChange += _action;
        public void UnregisterOnGrowthStageChange(Action _action) => m_onGrowthStageChange -= _action;

        public void IncreaseGrowthStage() => ChangeGrowthStage(1);
        public void DecreaseGrowthStage() => ChangeGrowthStage(-1);

        private void ChangeGrowthStage(int changeDirection)
        {
            int changedValue = m_CurrentGrowthStageIndex + (changeDirection / Mathf.Abs(changeDirection));
            if (changedValue > 0 && changedValue < m_GrowthStages.Length && m_CurrentGrowthStageIndex != changedValue)
            {
                m_CurrentGrowthStage = m_GrowthStages[changedValue];
                m_CurrentGrowthStageIndex = changedValue;
                m_onGrowthStageChange?.Invoke();
            }
            Debug.LogError("GrowthStage out of bounce or not changing");
        }

        public void ResetGrowth()
        {
            m_CurrentGrowthStage = m_GrowthStages[0];
            m_CurrentGrowthStageIndex = 0;
            m_onGrowthStageChange?.Invoke();
        }

        /// <summary>
        /// Harvest the Plant 
        /// </summary>
        /// <returns></returns>
        public bool Harvest(out int harvestedAmmount)
        {
            // IDEA harvest expierence could be change the ammount a bit
            harvestedAmmount = 0;

            if (m_CurrentGrowthStage.Harvestable)
            {
                harvestedAmmount = m_CurrentGrowthStage.HarvestAmount;

                switch (m_CurrentGrowthStage.HarvestType)
                {
                    case HarvestType.harvest:
                        m_MyGroundTile.RemovePlant();
                        return true;
                    case HarvestType.collect:
                        DecreaseGrowthStage();
                        return true;
                    default: return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Plants the plant, needs to get called for initialization
        /// </summary>
        /// <returns>GameObject Plant Visual</returns>
        public void Planting(GroundTile _myGroundTile)
        {
            m_CurrentGrowthStage = m_GrowthStages[m_CurrentGrowthStageIndex];
            m_MyGroundTile = _myGroundTile;
        }


    }

}
