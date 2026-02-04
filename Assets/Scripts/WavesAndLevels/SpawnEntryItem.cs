using System;
using UnityEngine;

namespace OrbReaper
{
    [Serializable]
    public class SpawnEntryItem
    {
        [SerializeField] 
        private EnemyTypes enemyType;
        
        [Min(1)]
        [SerializeField] 
        private int count;

        public int Count => count;

        public EnemyTypes EnemyType => enemyType;
    }
}