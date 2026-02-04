using UnityEngine;

namespace Characters
{
    public class UnityModel : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rigidbody;

        public Rigidbody Rigidbody => rigidbody;

        public Transform Transform => Rigidbody.transform;
    }
}