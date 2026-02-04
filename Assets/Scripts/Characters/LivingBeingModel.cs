using System;
using UnityEngine;

namespace OrbReaper
{
    [Serializable]
    public class LivingBeingModel
    {
        [SerializeField]
        private float maxHealth;
        
        [SerializeField]
        private float health;

        public float MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        public float Health
        {
            get => health;
            set => health = value;
        }
    }
}