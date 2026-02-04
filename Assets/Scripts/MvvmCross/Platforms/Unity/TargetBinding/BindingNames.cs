using UnityEngine.UI;

namespace MvvmCross.Platforms.Unity.TargetBinding
{
    public static class BindingNames
    {
        public const string Click = nameof(Click);
        
        public const string IsOn = nameof(IsOn);

        public static string BindButtonClick(this Button target) => Click;
        
        public static string BindIsOn(this Toggle target) => IsOn;
    }
}