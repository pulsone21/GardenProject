using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace GardenProject
{
    public enum GrowthStage { child, teenager, adult }

    [CreateAssetMenu(menuName = "ScriptableObjects/Plant")]
    public class Plant : ScriptableObject
    {
        private GrowthStage m_growthStage;
        private bool m_isHarvastable;
        [SerializeField] private int m_max_Harvest_Ammount;
        public string Name;

        public GameObject[] PlantVisuals = new GameObject[Enum.GetValues(typeof(GrowthStage)).Length];

        public Sprite UiVisual;

        public GrowthStage GrowthStage { get => m_growthStage; }
        public bool IsHarvastable { get => m_isHarvastable; }

        private Action<GrowthStage> m_onGrowthStageChange;

        public void RegisterOnGrowthStageChange(Action<GrowthStage> _action) => m_onGrowthStageChange += _action;
        public void UnregisterOnGrowthStageChange(Action<GrowthStage> _action) => m_onGrowthStageChange -= _action;

        public void IncreaseGrowthStage()
        {
            if ((int)m_growthStage < (int)Enum.GetValues(typeof(GrowthStage)).Cast<GrowthStage>().Max())
            {
                m_growthStage = (GrowthStage)(int)m_growthStage + 1;
                if ((int)m_growthStage >= 2)
                {
                    m_isHarvastable = true;
                }
                m_onGrowthStageChange?.Invoke(m_growthStage);
            }
            Debug.LogError("GrowthStage is already at max state");
        }

        public void ResetGrowth()
        {
            m_growthStage = GrowthStage.child;
            m_isHarvastable = false;
        }

        /// <summary>
        /// Harvest the Plant 
        /// </summary>
        /// <returns></returns>
        public bool Harvest(out int harvestedAmmount)
        {
            // IDEA harvest expierence could be change the ammount a bit

            harvestedAmmount = 0;
            if (!m_isHarvastable) return false;

            switch (m_growthStage)
            {
                case GrowthStage.child:
                    //TODO Create some Error UI
                    Debug.LogError("Cannont harvest a child plant");
                    return false;
                case GrowthStage.teenager:
                    harvestedAmmount = Mathf.FloorToInt(m_max_Harvest_Ammount * .5f);
                    ResetGrowth();
                    return true;
                case GrowthStage.adult:
                    harvestedAmmount = m_max_Harvest_Ammount;
                    ResetGrowth();
                    return true;
                default:
                    Debug.LogError($"{m_growthStage} state isn't Implemented");
                    return false;
            }
        }



    }

}
