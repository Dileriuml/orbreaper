using UnityEngine;

namespace OrbReaper.Utils
{
    public static class CollisionExtensions
    {
        public static float GetKineticEnergy(this Collision collision)
        {
            var relativeVelocity = collision.relativeVelocity;
            // Get the mass of the other collider
            var otherMass = collision.gameObject.GetComponent<Rigidbody>().mass;
            // Calculate the kinetic energy of the collision
            return 0.5f * otherMass * Mathf.Pow(relativeVelocity.magnitude, 2);
        }
    }
}