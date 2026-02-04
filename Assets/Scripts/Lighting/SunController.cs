using UnityEngine;

namespace OrbReaper.Lighting
{
    [RequireComponent(typeof(Animator))]
    public class SunController : MonoBehaviour, ISunController
    {
        private const string SunTrigger = "SunTrigger";
        
        [SerializeField]
        private Animator sunAnimator;
        
        public void Trigger()
        {
            if (!sunAnimator)
            {
                return;
            }

            sunAnimator.SetTrigger(SunTrigger);
        }
    }
}