using UnityEngine;

namespace OrbReaper.Utils
{
    public class RoundGoing : MonoBehaviour
    {
        [SerializeField]
        private Transform center;

        [SerializeField]
        private float speed;
        
        private void Update()
        {
            transform.transform.RotateAround(center.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}