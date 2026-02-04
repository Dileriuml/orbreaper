using UnityEngine;

namespace OrbReaper.Utils
{
    public class TransformBinder : MonoBehaviour
    {
        [SerializeField]
        private GameObject otherObject;

        [SerializeField]
        private bool bindRotation = true;
        
        [SerializeField]
        private bool bindPosition = true;

        [SerializeField]
        private Vector3 offset = Vector3.zero;
        
        private void Update()
        {
            if (!otherObject)
            {
                return;
            }

            if (bindPosition)
            {
                gameObject.transform.position = otherObject.transform.position + offset;
            }

            if (bindRotation)
            {
                gameObject.transform.rotation = otherObject.transform.rotation;
            }
        }
    }
}