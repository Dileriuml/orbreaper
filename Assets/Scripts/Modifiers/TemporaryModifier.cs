using System;
using UnityEngine;

namespace OrbReaper.Modifiers
{
    [Serializable]
    public abstract class TemporaryModifier : FloatModifier
    {
        [SerializeField]
        private float duration;

        [SerializeField]
        private bool isInfinite;

        public bool IsInfinite => isInfinite;
        
        public float Duration => duration;

        public float StartTime { get; set; }

        /// <summary>
        /// Note that this one depends time-step
        /// </summary>
        public bool IsExpired => !isInfinite && Time.time - StartTime > duration;
    }
}