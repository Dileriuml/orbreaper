using System;
using UnityEngine;

namespace OrbReaper.Player
{
    [Serializable]
    public class CharacterModel
    {
        [SerializeField] private float movePower = 1;
        
        [SerializeField] private float soulPower = 200;
        
        [SerializeField] private float maxSoulPower = 1000;

        public float MovePower => movePower;

        public float SoulPower => soulPower;
        
        public float MaxSoulPower => maxSoulPower;
    }
}