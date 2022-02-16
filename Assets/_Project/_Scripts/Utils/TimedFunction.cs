using System;
using UnityEngine;

namespace Utilities
{
    public class TimedFunction
    {

        public static TimedFunction Create(Action action, float timer)
        {
            GameObject gO = new GameObject("TimedFunction", typeof(MonoBehaviourHook));
            TimedFunction timedFunc = new TimedFunction(action, timer, gO);
            gO.GetComponent<MonoBehaviourHook>().onUpdate = timedFunc.Update;
            return timedFunc;
        }

        private Action action;
        private float timer;
        private bool isDestroyed;
        private GameObject gameObject;

        public class MonoBehaviourHook : MonoBehaviour
        {
            public Action onUpdate;
            private void Update()
            {
                if (onUpdate != null) onUpdate();
            }
        }

        private TimedFunction(Action action, float timer, GameObject gameObject)
        {
            this.action = action;
            this.timer = timer;
            this.gameObject = gameObject;
            this.isDestroyed = false;
        }

        public void Update()
        {
            if (!isDestroyed)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    action();
                    DestroySelf();
                }
            }
        }

        public void CancleAction()
        {
            DestroySelf();
        }

        private void DestroySelf()
        {
            isDestroyed = true;
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}