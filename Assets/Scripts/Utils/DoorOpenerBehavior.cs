using UnityEngine;

namespace OrbReaper.Utils
{
    public class DoorOpenerBehavior : MonoBehaviour
    {
        [SerializeField]
        private Animator doorAnimator;

        private void OnTriggerEnter(Collider other)
        {
            doorAnimator.SetTrigger("PlayerProximity");
        }

        private void OnTriggerExit(Collider other)
        {
            doorAnimator.SetTrigger("PlayerProximity");
        }
    }
}