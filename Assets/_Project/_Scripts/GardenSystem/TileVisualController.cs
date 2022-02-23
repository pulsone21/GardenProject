using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GardenProject
{
    public class TileVisualController : MonoBehaviour
    {
        [SerializeField] private GameObject m_grasTile;
        [SerializeField] private GameObject m_soilTile;


        public void VisualChangeOnPlantableChange(bool IsPlantable)
        {
            if (IsPlantable)
            {
                m_grasTile.SetActive(false);
                m_soilTile.SetActive(true);
            }
            else
            {
                m_grasTile.SetActive(true);
                m_soilTile.SetActive(false);
            }
        }

    }
}
