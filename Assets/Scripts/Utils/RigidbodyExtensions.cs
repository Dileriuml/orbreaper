using UnityEngine;

namespace OrbReaper.Utils
{
    public static class RigidbodyExtensions
    {
        public static void ResetVelocity(this Rigidbody rigidbody)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}