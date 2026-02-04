using System;
using UnityEngine;

namespace OrbReaper.Modifiers
{
    [Serializable]
    public class MultiplyModifier : TemporaryModifier
    {
        [SerializeField]
        private float multiplier;
        
        public override float ModifyValue(float modifyValue) => modifyValue * multiplier;
    }
}