using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MvvmCross.Platforms.Unity.TargetBinding
{
    public class ToggleTargetBinding : MvxConvertingTargetBinding<Toggle, bool>
    {
        private UnityAction<bool> valueChangedhandler;
        
        public ToggleTargetBinding(Toggle target) : base(target)
        {
        }

        protected override void SetValueImpl(Toggle target, bool value) => target.isOn = value;

        public override void SubscribeToEvents()
        {
            var uiSwitch = Target;
            if (uiSwitch == null)
            {
                return;
            }

            valueChangedhandler = HandleValueChanged;
            uiSwitch.onValueChanged.AddListener(valueChangedhandler);
            
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (!isDisposing) return;

            Target.onValueChanged.RemoveListener(valueChangedhandler);
            valueChangedhandler = null;
        }

        private void HandleValueChanged(bool value) => FireValueChanged(value);
    }
}