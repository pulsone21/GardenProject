using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace GardenProject
{
    public class HoverUI : MonoBehaviour
    {
        [SerializeField] private float m_blinkTimer;
        [SerializeField] private Vector3 m_MaxScale;
        [SerializeField] private Transform m_Visuals_Transform;
        private Sequence tween;

        private void OnEnable()
        {
            tween = DOTween.Sequence();
            tween.Append(m_Visuals_Transform.DOScale(m_MaxScale, m_blinkTimer).SetEase(Ease.InOutSine));
            tween.SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            tween.Kill();
            m_Visuals_Transform.localScale = new Vector3(1.9f, 0, 1.9f);
        }
    }
}
