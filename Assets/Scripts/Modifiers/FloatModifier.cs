using System;
using UnityEngine;

namespace OrbReaper.Modifiers
{
    [Serializable]
    public abstract class FloatModifier
    {
        [SerializeField]
        private string name;
        
        public string Name => name;
        
        public abstract float ModifyValue(float modifyValue);
    }
}