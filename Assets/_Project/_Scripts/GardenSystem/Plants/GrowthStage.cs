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
        [SerializeField, Range(1, 15), Tooltip("how many DAYS this growth stage needs to finish")] private float m_GrowthTimeLength;

        private const int MINUTES_PER_DAY = 1440;
        public GrowthStage(int _harvestAmmount, GameObject _visuals, bool _harvestable, HarvestType _harvestType, float _GrowthTime)
        {
            m_HarvestAmount = _harvestAmmount;
            m_Visuals = _visuals;
            m_Harvestable = _harvestable;
            m_harvestType = _harvestType;
            m_GrowthTimeLength = _GrowthTime;
        }

        public bool Harvestable => m_Harvestable;
        public int HarvestAmount => m_HarvestAmount;
        public GameObject Visual => m_Visuals;
        public HarvestType HarvestType => m_harvestType;

        /// <summary>
        /// GrwothTime in minutes
        /// </summary>
        public float GrowthTime => m_GrowthTimeLength * MINUTES_PER_DAY;
    }
}
