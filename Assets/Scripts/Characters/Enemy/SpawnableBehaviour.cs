using System;
using UnityEngine;
using Zenject;

namespace Characters
{
    public abstract class SpawnableBehaviour : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool currentPool;
        
        public void Dispose()
        {
            currentPool.Despawn(this);
        }

        public void OnDespawned()
        {
            currentPool = null;
            OnDespawn();
        }
        
        public void OnSpawned(IMemoryPool pool)
        {
            currentPool = pool;
            OnSpawn();
        }

        protected virtual void OnSpawn()
        {
        }

        protected virtual void OnDespawn()
        {
        }
    }
}