using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TimeSystem
{
    public class UITimeController : MonoBehaviour
    {
        [SerializeField] private TimeManager m_TimeManager;
        [SerializeField] private TextMeshProUGUI m_TimeStampLable;


        public void SetSpeedModifier(int amount) => m_TimeManager.ChangeSpeedModifier(amount);

        public void PauseTime() => m_TimeManager.PauseTime();

        private void Awake() => m_TimeManager.RegisterForTimeUpdate(UpdateLable, TimeManager.SubscriptionType.AfterElapse);

        private void Start() => UpdateLable(m_TimeManager.CurrentTimeStamp);

        private void UpdateLable(TimeStamp _timeStamp) => m_TimeStampLable.text = _timeStamp.ToString();

        private void OnDestroy() => m_TimeManager.UnregisterForTimeUpdate(UpdateLable, TimeManager.SubscriptionType.AfterElapse);
    }
}
