using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GardenProject
{
    public enum HarvestType { none, harvest, collect }
    [System.Serializable]
    public class GrowthStage
    {
        [SerializeField] private int m_HarvestAmount;
        [SerializeField] private GameObject m_Visuals;
        [SerializeField] private bool m_Harvestable;
        [SerializeField] private HarvestType m_harvestType;

        public GrowthStage(int _harvestAmmount, GameObject _visuals, bool _harvestable, HarvestType _harvestType)
        {
            m_HarvestAmount = _harvestAmmount;
            m_Visuals = _visuals;
            m_Harvestable = _harvestable;
            m_harvestType = _harvestType;
        }

        public bool Harvestable => m_Harvestable;
        public int HarvestAmount => m_HarvestAmount;
        public GameObject Visual => m_Visuals;
        public HarvestType HarvestType => m_harvestType;
    }
}
