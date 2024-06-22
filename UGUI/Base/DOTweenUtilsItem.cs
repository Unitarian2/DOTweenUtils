
using System;

using UnityEngine;

namespace DOTweenUtils
{
    public abstract class DOTweenUtilsItem<T> : MonoBehaviour where T : DOTweenUtilsItem<T>
    {
        [Tooltip("If TRUE, Perform method will be called automatically on Start")]
        [SerializeField] protected bool performOnStart;

        protected Action OnComplete;

        public abstract T Perform();

        public abstract void Stop(bool complete);

        public T OnCompleted(Action OnComplete)
        {
            this.OnComplete = OnComplete;
            return (T)this;
        }
    }
}

