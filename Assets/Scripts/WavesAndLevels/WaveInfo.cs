using System;
using System.Collections;
using UnityEngine;

namespace OrbReaper
{
    [Serializable]
    public class WaveInfo
    {
        private string name;
        
        [SerializeField]
        private SpawnEntryItem[] spawnEntries;

        public SpawnEntryItem[] SpawnEntries => spawnEntries;
    }
}