using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GardenProject
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class AdjustGridSize : MonoBehaviour
    {
        public enum AdjustingType { height, width, both }
        [SerializeField] private GridLayoutGroup m_GridLayOutGroup;
        [SerializeField] private RectTransform m_RectTransform;
        [SerializeField] private AdjustingType adjustingType;
        private int m_LastChildCount = 0;


        private void Update()
        {
            if (m_LastChildCount != m_RectTransform.childCount)
            {
                m_LastChildCount = m_RectTransform.childCount;
                float newX = adjustingType != AdjustingType.height ? CalculateWidth() : m_RectTransform.sizeDelta.x;
                float newY = adjustingType != AdjustingType.width ? CalculateHeight() : m_RectTransform.sizeDelta.y;
                m_RectTransform.sizeDelta = new Vector2(newX, newY);
            }
        }

        private float CalculateHeight()
        {
            float cellHeight = m_GridLayOutGroup.cellSize.y;
            int rowCount = Mathf.CeilToInt((float)m_LastChildCount / m_GridLayOutGroup.constraintCount);
            return (rowCount * (cellHeight + m_GridLayOutGroup.spacing.y)) + m_GridLayOutGroup.padding.vertical;
        }

        private float CalculateWidth()
        {
            float cellWidth = m_GridLayOutGroup.cellSize.x;
            int colCount = m_LastChildCount >= 5 ? 5 : m_LastChildCount;
            return (colCount * (cellWidth + m_GridLayOutGroup.spacing.x)) + m_GridLayOutGroup.padding.horizontal;
        }
    }
}
