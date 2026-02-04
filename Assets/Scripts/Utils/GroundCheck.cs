using System;
using Characters;
using UnityEngine;
using Zenject;

public class GroundCheck : ITickable
{
    private readonly float checkDistance; // distance to check for ground
    private readonly LayerMask groundLayers;
    private readonly UnityModel unityModel;
    
    // layers to check for ground
    public GroundCheck(UnityModel unityModel, Settings settings)
    {
        this.unityModel = unityModel;
        checkDistance = settings.CheckDistanse;
        groundLayers = settings.GroundLayers;
    }
    
    public void Tick()
    {
        // shoot a ray from the center of the object downwards
        var ray = new Ray(unityModel.Transform.position, Vector3.down);
        
        // This one is for debug
        Debug.DrawRay(ray.origin, ray.direction * checkDistance, Color.red);
        // check if the ray hits any colliders on the ground layers
        if (Physics.Raycast(ray, out _, checkDistance, groundLayers))
        {
            // if the ray hit something, the object is grounded
            IsGrounded = true;
        }
        else
        {
            // if the ray did not hit anything, the object is not grounded
            IsGrounded = false;
        }
    }

    public bool IsGrounded { get; private set; }

    [Serializable]
    public class Settings
    {
        public float CheckDistanse;
        public LayerMask GroundLayers;
    }
}