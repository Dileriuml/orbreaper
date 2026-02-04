using System;
using UnityEngine;

namespace OrbReaper.Modifiers
{
    [Serializable]
    public class AddModifier : TemporaryModifier
    {
        [SerializeField]
        private float addValue;

        public override float ModifyValue(float modifyValue) => modifyValue + addValue;
    }
}